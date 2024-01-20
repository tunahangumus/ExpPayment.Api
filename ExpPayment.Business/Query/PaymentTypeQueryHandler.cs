using AutoMapper;
using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data.Entity;
using ExpPayment.Data;
using ExpPayment.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpPayment.Business.Query;

public class PaymentTypeQueryHandler :
	IRequestHandler<GetAllPaymentTypeQuery, ApiResponse<List<PaymentTypeResponse>>>,
	IRequestHandler<GetPaymentTypeByIdQuery, ApiResponse<PaymentTypeResponse>>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;

	public PaymentTypeQueryHandler(ExpPaymentDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse<List<PaymentTypeResponse>>> Handle(GetAllPaymentTypeQuery request,
		CancellationToken cancellationToken)
	{
		var list = await dbContext.Set<PaymentType>().Where(x=>x.IsActive==true).ToListAsync(cancellationToken);
		var mappedList = mapper.Map<List<PaymentType>, List<PaymentTypeResponse>>(list);
		return new ApiResponse<List<PaymentTypeResponse>>(mappedList);
	}

	public async Task<ApiResponse<PaymentTypeResponse>> Handle(GetPaymentTypeByIdQuery request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<PaymentType>().Where(x=> x.Id == request.PaymentTypeId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if(entity == null)
		{
			return new ApiResponse<PaymentTypeResponse>("There is no such PaymentType with given id.");
		}
		var mapped = mapper.Map<PaymentType, PaymentTypeResponse>(entity);
		return new ApiResponse<PaymentTypeResponse>(mapped);
	}
}