using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpPayment.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminPaymentDemandsController : ControllerBase
	{
		private readonly IMediator mediator;

		public AdminPaymentDemandsController(IMediator mediator)
		{
			this.mediator = mediator;
		}


		[HttpGet("ActiveDemand")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<PaymentDemandResponse>>> GetActiveDemand()
		{
			var operation = new AdminGetAllActivePaymentDemandQuery();
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpGet("PassiveDemand")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<PaymentDemandResponse>>> GetPassiveDemand()
		{
			var operation = new AdminGetAllPassivePaymentDemandQuery();
			var result = await mediator.Send(operation);
			return result;
		}


		[HttpGet("DemandByPersonelId/{id}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<PaymentDemandResponse>>> GetDemandById(int id)
		{
			var operation = new AdminGetAllActivePaymentDemandByPersonelQuery(id);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost("DemandApproval")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse> Post(AdminPaymentDemandApproval request,int paymentDemandId)
		{
			string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			if (request.IsApproved)
			{
				var operation = new AdminPaymentDemandApproveCommand(paymentDemandId,int.Parse(id),request);
				var result = await mediator.Send(operation);
				return result;
			}
			else
			{
				var operation = new AdminPaymentDemandRejectCommand(paymentDemandId, int.Parse(id), request);
				var result = await mediator.Send(operation);
				return result;
			}
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse> EditDemand(int id, [StringLength(maximumLength: 90)] string description)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new AdminPaymentDemandEditCommand(id,int.Parse(userId), description);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "personel")]
		public async Task<ApiResponse> DeleteDemand(int id)
		{
			string userId = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
			var operation = new AdminDeletePaymentDemandCommand(id,int.Parse(userId));
			var result = await mediator.Send(operation);
			return result;
		}

	}
}
