﻿using ExpPayment.Base.Response;
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
	public class ReportsController : ControllerBase
	{
		private readonly IMediator mediator;

		public ReportsController(IMediator mediator)
		{
			this.mediator = mediator;
		}


		[HttpGet("GetPersonelTransactions")]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse<List<PersonelExpenseReport>>> GetPersonelTransaction()
		{
			string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new GetPersonelTransactionQuery(int.Parse(id));
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("GetCompanyPayments")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<CompanyPaymentReport>>> GetCompanyPayment()
		{
			var operation = new GetCompanyAllPaymentQuery();
			var result = await mediator.Send(operation);
			return result;
		}
		
		[HttpGet("GetCompanyExpenseByPersonel")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<CompanyExpenseByPersonel>>> GetExpenseByPersonel()
		{
			var operation = new GetExpenseByPersonelIdQuery();
			var result = await mediator.Send(operation);
			return result;
		}


	}
}
