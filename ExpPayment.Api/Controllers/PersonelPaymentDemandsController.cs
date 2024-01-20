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
	public class PersonelPaymentDemandsController : ControllerBase
	{
		private readonly IMediator mediator;

		public PersonelPaymentDemandsController(IMediator mediator)
		{
			this.mediator = mediator;
		}


		[HttpGet("ActiveDemand")]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse<List<PaymentDemandResponse>>> GetActiveDemand()
		{
			string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new GetAllActivePaymentDemandQuery(int.Parse(id));
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpGet("PassiveDemand")]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse<List<PaymentDemandResponse>>> GetPassiveDemand()
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new GetAllPassivePaymentDemandQuery(int.Parse(userId));
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpGet("{id}")]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse<PaymentDemandResponse>> GetDemandById(int id)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new GetPaymentDemandByIdQuery(int.Parse(userId), id);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse> CreateDemand(PaymentDemandRequest request)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new CreatePaymentDemandCommand(request, int.Parse(userId));
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse> EditDemand(int id,PaymentDemandRequest request)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new UpdatePaymentDemandCommand(int.Parse(userId),id, request);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse> DeleteDemand(int id)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new DeletePaymentDemandCommand(int.Parse(userId), id);
			var result = await mediator.Send(operation);
			return result;
		}

	}
}

