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

public class PaymentCategoryQueryHandler :
	IRequestHandler<GetAllPaymentCategoryQuery, ApiResponse<List<PaymentCategoryResponse>>>,
	IRequestHandler<GetPaymentCategoryByIdQuery, ApiResponse<PaymentCategoryResponse>>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;

	public PaymentCategoryQueryHandler(ExpPaymentDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse<List<PaymentCategoryResponse>>> Handle(GetAllPaymentCategoryQuery request,
		CancellationToken cancellationToken)
	{
		var list = await dbContext.Set<PaymentCategory>().Where(x => x.IsActive == true).ToListAsync(cancellationToken);
		var mappedList = mapper.Map<List<PaymentCategory>, List<PaymentCategoryResponse>>(list);
		return new ApiResponse<List<PaymentCategoryResponse>>(mappedList);
	}

	public async Task<ApiResponse<PaymentCategoryResponse>> Handle(GetPaymentCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<PaymentCategory>().Where(x => x.Id == request.PaymentCategoryId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity == null)
		{
			return new ApiResponse<PaymentCategoryResponse>("There is no such PaymentCategory with given id.");
		}
		var mapped = mapper.Map<PaymentCategory, PaymentCategoryResponse>(entity);
		return new ApiResponse<PaymentCategoryResponse>(mapped);
	}
}