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
	public class PaymentCategoriesController : ControllerBase
	{
		private readonly IMediator mediator;

		public PaymentCategoriesController(IMediator mediator)
		{
			this.mediator = mediator;
		}


		[HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<PaymentCategoryResponse>>> Get()
		{
			string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new GetAllPaymentCategoryQuery();
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpGet("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<PaymentCategoryResponse>> GetById(int id)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new GetPaymentCategoryByIdQuery(id);
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<PaymentCategoryResponse>> Post(PaymentCategoryRequest request)
		{
			string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new CreatePaymentCategoryCommand(request, int.Parse(id));
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpPut("{id}")]
		public async Task<ApiResponse> Put(int id, PaymentCategoryRequest request)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new UpdatePaymentCategoryCommand(int.Parse(userId), id, request);
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpDelete("{id}")]
		public async Task<ApiResponse> DeleteAsync(int id)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new DeletePaymentCategoryCommand(int.Parse(userId), id);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
