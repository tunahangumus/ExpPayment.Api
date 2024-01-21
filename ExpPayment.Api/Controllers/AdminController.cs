using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpPayment.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IMediator mediator;

		public AdminController(IMediator mediator)
		{
			this.mediator = mediator;
		}
		[HttpGet("CreatePersonel")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse> GetActiveDemand(ApplicationUserRequest userRequest)
		{
			var operation = new UserCreateCommand(userRequest);
			var result = await mediator.Send(operation);
			return result;
		}
		
		[HttpPost("Login")]
		public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
		{
			var operation = new CreateTokenCommand(request);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
