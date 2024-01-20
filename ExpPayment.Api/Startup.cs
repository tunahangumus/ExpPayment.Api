using AutoMapper;
using ExpPayment.Api.Middleware;
using ExpPayment.Base.Dapper;
using ExpPayment.Base.Token;
using ExpPayment.Business.Cqrs;
using ExpPayment.Business.Mapper;
using ExpPayment.Business.Validator;
using ExpPayment.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
namespace ExpPayment.Api;


public class Startup
{
	public IConfiguration Configuration;

	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		string connection = Configuration.GetConnectionString("PostgresSqlConnection");
		services.AddDbContext<ExpPaymentDbContext>(options => options.UseNpgsql(connection));

		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTokenCommand).GetTypeInfo().Assembly));

		services.AddControllers()
			.AddFluentValidation(x =>
		{
			x.RegisterValidatorsFromAssemblyContaining<CreateExpenseValidator>();
			x.RegisterValidatorsFromAssemblyContaining<CreatePaymentDemandValidator>();
			x.RegisterValidatorsFromAssemblyContaining<CreatePaymentTypeValidator>();
			x.RegisterValidatorsFromAssemblyContaining<CreatePaymentDemandValidator>();
			x.RegisterValidatorsFromAssemblyContaining<CreatePaymentCategoryValidator>();
		});

		var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
		services.AddSingleton(mapperConfig.CreateMapper());

		services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vb Api Management", Version = "v1.0" });

			var securityScheme = new OpenApiSecurityScheme
			{
				Name = "Vb Management for IT Company",
				Description = "Enter JWT Bearer token **_only_**",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = "bearer",
				BearerFormat = "JWT",
				Reference = new OpenApiReference
				{
					Id = JwtBearerDefaults.AuthenticationScheme,
					Type = ReferenceType.SecurityScheme
				}
			};
			c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
			c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{ securityScheme, new string[] { } }
			});
		});
		services.AddAuthorization();

		JwtConfig jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
		services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

		services.AddAuthentication(x =>
		{
			x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(x =>
		{
			x.RequireHttpsMetadata = true;
			x.SaveToken = true;
			x.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = jwtConfig.Issuer,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
				ValidAudience = jwtConfig.Audience,
				ValidateAudience = false,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.FromMinutes(2)
			};
			x.Events = new JwtBearerEvents
			{
				OnAuthenticationFailed = context =>
				{
					// Handle authentication failure here
					context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					context.Response.ContentType = "application/json";
					context.Response.WriteAsync(JsonSerializer.Serialize("Authentication failed!"));

					return Task.CompletedTask;
				}
			};
		});

	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.UseMiddleware<ErrorHandlerMiddleware>();
		app.UseHttpsRedirection();
		app.UseAuthentication();
		app.UseRouting();
		app.UseAuthorization();
		app.UseEndpoints(x => { x.MapControllers(); });
	}
}
