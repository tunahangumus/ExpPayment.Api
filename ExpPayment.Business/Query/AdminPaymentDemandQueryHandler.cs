using AutoMapper;
using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data.Entity;
using ExpPayment.Data;
using ExpPayment.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExpPayment.Business.Query;

public class AdminPaymentDemandQueryHandler :
	IRequestHandler<AdminGetAllActivePaymentDemandQuery, ApiResponse<List<PaymentDemandResponse>>>,
	IRequestHandler<AdminGetAllPassivePaymentDemandQuery, ApiResponse<List<PaymentDemandResponse>>>,
	IRequestHandler<AdminGetAllActivePaymentDemandByPersonelQuery, ApiResponse<List<PaymentDemandResponse>>>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;

	public AdminPaymentDemandQueryHandler(ExpPaymentDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse<List<PaymentDemandResponse>>> Handle(AdminGetAllActivePaymentDemandQuery request, CancellationToken cancellationToken)
	{
		var list = await dbContext.Set<PaymentDemand>().Where(x => x.IsActive == true).ToListAsync(cancellationToken);
		var mappedList = mapper.Map<List<PaymentDemand>, List<PaymentDemandResponse>>(list);
		return new ApiResponse<List<PaymentDemandResponse>>(mappedList);
	}

	public async Task<ApiResponse<List<PaymentDemandResponse>>> Handle(AdminGetAllPassivePaymentDemandQuery request, CancellationToken cancellationToken)
	{
		var list = await dbContext.Set<PaymentDemand>().Where(x => x.IsActive == false).ToListAsync(cancellationToken);
		var mappedList = mapper.Map<List<PaymentDemand>, List<PaymentDemandResponse>>(list);
		return new ApiResponse<List<PaymentDemandResponse>>(mappedList);
	}

	public async Task<ApiResponse<List<PaymentDemandResponse>>> Handle(AdminGetAllActivePaymentDemandByPersonelQuery request, CancellationToken cancellationToken)
	{
		var list = await dbContext.Set<PaymentDemand>().Where(x => x.InsertUserId==request.userId && x.IsActive == true ).ToListAsync(cancellationToken);
		var mappedList = mapper.Map<List<PaymentDemand>, List<PaymentDemandResponse>>(list);
		return new ApiResponse<List<PaymentDemandResponse>>(mappedList);
	}
}