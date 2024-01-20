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
	public class PaymentTypesController : ControllerBase
	{
		private readonly IMediator mediator;

		public PaymentTypesController(IMediator mediator)
		{
			this.mediator = mediator;
		}


		[HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<PaymentTypeResponse>>> Get()
		{
			string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new GetAllPaymentTypeQuery();
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpGet("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<PaymentTypeResponse>> GetById(int id)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new GetPaymentTypeByIdQuery(id);
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<PaymentTypeResponse>> Post(PaymentTypeRequest request)
		{
			string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new CreatePaymentTypeCommand(request, int.Parse(id));
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpPut("{id}")]
		public async Task<ApiResponse> Put(int id, PaymentTypeRequest request)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new UpdatePaymentTypeCommand(int.Parse(userId), id, request);
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpDelete("{id}")]
		public async Task<ApiResponse> DeleteAsync(int id)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new DeletePaymentTypeCommand(int.Parse(userId), id);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
