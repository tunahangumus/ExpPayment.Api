using ExpPayment.Base.Encryption;
using ExpPayment.Base.Response;
using ExpPayment.Base.Token;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data.Entity;
using ExpPayment.Data;
using ExpPayment.Schema;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;

namespace ExpPayment.Business.Command;

public class AdminCommandHandler:IRequestHandler<UserCreateCommand, ApiResponse>,
	IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly JwtConfig jwtConfig;
	private readonly IMapper mapper;
	public AdminCommandHandler(ExpPaymentDbContext dbContext, IOptionsMonitor<JwtConfig> jwtConfig, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.jwtConfig = jwtConfig.CurrentValue;
		this.mapper = mapper;
	}

	public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
	{
		var user = await dbContext.Set<ApplicationUser>().Where(x => x.UserName == request.Model.UserName)
			.FirstOrDefaultAsync(cancellationToken);
		if (user == null)
		{
			return new ApiResponse<TokenResponse>("Invalid user information");
		}
		string hash = Md5Extension.GetHash(request.Model.Password.Trim());
		if (hash != user.Password)
		{
			user.LastActivityDate = DateTime.UtcNow;
			user.PasswordRetryCount++;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse<TokenResponse>("Invalid user information");
		}
		if (user.Status != 1)
		{
			return new ApiResponse<TokenResponse>("Invalid user status");
		}
		if (user.PasswordRetryCount > 3)
		{
			return new ApiResponse<TokenResponse>("Invalid user status");
		}
		user.LastActivityDate = DateTime.UtcNow;
		user.PasswordRetryCount = 0;
		await dbContext.SaveChangesAsync(cancellationToken);
		string token = Token(user);
		return new ApiResponse<TokenResponse>(new TokenResponse()
		{
			Email = user.Email,
			Token = token,
			ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration)
		});
	}

	private string Token(ApplicationUser user)
	{
		Claim[] claims = GetClaims(user);
		var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

		var jwtToken = new JwtSecurityToken(
			jwtConfig.Issuer,
			jwtConfig.Audience,
			claims,
			expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
			signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
		);

		string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
		return accessToken;
	}

	private Claim[] GetClaims(ApplicationUser user)
	{
		var claims = new[]
		{
			new Claim("Id", user.Id.ToString()),
			new Claim("Email", user.Email),
			new Claim("UserName", user.UserName),
			new Claim(ClaimTypes.Role, user.Role)
		};

		return claims;
	}

	public async Task<ApiResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
	{
		var user = await dbContext.Set<ApplicationUser>().Where(x => x.UserName == request.Model.UserName)
			.FirstOrDefaultAsync(cancellationToken);
		if (user != null)
		{
			return new ApiResponse("Username has taken");
		}
		else
		{
			string hash = Md5Extension.GetHash(request.Model.Password.Trim());
			var entity = mapper.Map<ApplicationUserRequest, ApplicationUser>(request.Model);
			entity.Password = hash;
			entity.InsertDate = DateTime.Now;
			await dbContext.SaveChangesAsync();
			return new ApiResponse("");
		}
	}
}
