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


public class PersonelPaymentDemandQueryHandler :
	IRequestHandler<GetAllActivePaymentDemandQuery, ApiResponse<List<PaymentDemandResponse>>>,
	IRequestHandler<GetAllPassivePaymentDemandQuery, ApiResponse<List<PaymentDemandResponse>>>,
	IRequestHandler<GetPaymentDemandByIdQuery, ApiResponse<PaymentDemandResponse>>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;

	public PersonelPaymentDemandQueryHandler(ExpPaymentDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse<List<PaymentDemandResponse>>> Handle(GetAllPassivePaymentDemandQuery request, CancellationToken cancellationToken)
	{
		var list = await dbContext.Set<PaymentDemand>().Where(x => x.InsertUserId == request.id && x.IsActive == false).Include(x => x.Expense).Include(x => x.PaymentCategory).Include(x => x.PaymentType).Include(x => x.Invoice).ToListAsync(cancellationToken);
		var tupleList = list.Select(x => Tuple.Create(x.Expense, x)).ToList();
		var mappedList = mapper.Map<List<Tuple<Expense, PaymentDemand>>, List<PaymentDemandResponse>>(tupleList);
		return new ApiResponse<List<PaymentDemandResponse>>(mappedList);
	}

	public async Task<ApiResponse<List<PaymentDemandResponse>>> Handle(GetAllActivePaymentDemandQuery request, CancellationToken cancellationToken)
	{
		var list = await dbContext.Set<PaymentDemand>().Where(x => x.InsertUserId == request.id && x.IsActive == true).Include(x => x.Expense).Include(x => x.PaymentCategory).Include(x => x.PaymentType).Include(x => x.Invoice).ToListAsync(cancellationToken);
		var tupleList = list.Select(x => Tuple.Create(x.Expense, x)).ToList();
		var mappedList = mapper.Map<List<Tuple<Expense, PaymentDemand>>, List<PaymentDemandResponse>>(tupleList);
		return new ApiResponse<List<PaymentDemandResponse>>(mappedList);
	}

	public async Task<ApiResponse<PaymentDemandResponse>> Handle(GetPaymentDemandByIdQuery request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<PaymentDemand>().Where(x => x.InsertUserId == request.userId && x.IsActive == true && x.Id == request.PaymentDemandId).Include(x => x.Expense).FirstOrDefaultAsync(cancellationToken);
		var tuple = new Tuple<Expense, PaymentDemand>(entity.Expense, entity);
		var mappedList = mapper.Map<Tuple<Expense, PaymentDemand>, PaymentDemandResponse>(tuple);
		return new ApiResponse<PaymentDemandResponse>(mappedList);
	}




}