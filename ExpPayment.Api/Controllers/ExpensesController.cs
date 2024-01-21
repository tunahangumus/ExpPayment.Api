using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data.Entity;
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
	public class ExpensesController : ControllerBase
	{
		private readonly IMediator mediator;

		public ExpensesController(IMediator mediator)
		{
			this.mediator = mediator;
		}
		

		[HttpGet]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse<List<ExpenseResponse>>> Get()
		{
			string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new GetAllExpenseQuery(int.Parse(id));
			var result = await mediator.Send(operation);
			return result;
		}

		// GET api/<ExpensesController>/5
		[HttpGet("GetById")]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse<ExpenseResponse>> GetById([FromQuery]int id)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new GetExpenseByIdQuery(int.Parse(userId),id);
			var result = await mediator.Send(operation);
			return result;
		}

		// POST api/<ExpensesController>
		[HttpPost]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse<ExpenseResponse>> Post(ExpenseRequest request)
		{
			string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new CreateExpenseCommand(request, int.Parse(id));
			var result = await mediator.Send(operation);
			return result;
		}

		// PUT api/<ExpensesController>/5
		[HttpPut]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse> Put([FromQuery] int id,ExpenseRequest request)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new UpdateExpenseCommand(int.Parse(userId), id,request);
			var result = await mediator.Send(operation);
			return result;
		}

		// DELETE api/<ExpensesController>/5
		[HttpDelete]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse> DeleteAsync([FromQuery] int id)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new DeleteExpenseCommand(int.Parse(userId), id);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
