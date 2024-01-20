using AutoMapper;
using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data;
using ExpPayment.Data.Entity;
using ExpPayment.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Query;

public class ExpenseQueryHandler :
	IRequestHandler<GetAllExpenseQuery, ApiResponse<List<ExpenseResponse>>>,
	IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;

	public ExpenseQueryHandler(ExpPaymentDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpenseQuery request,
		CancellationToken cancellationToken)
	{
		var list = await dbContext.Set<Expense>().Where(x=>x.PersonelId == request.id && x.IsActive == true).ToListAsync(cancellationToken);
		var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
		return new ApiResponse<List<ExpenseResponse>>(mappedList);
	}

	public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<Expense>().Where(x => x.PersonelId == request.userId && x.Id == request.expenseId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity == null)
		{
			return new ApiResponse<ExpenseResponse>("There is no such Expense with given id.");
		}
		var mapped = mapper.Map<Expense, ExpenseResponse>(entity);
		return new ApiResponse<ExpenseResponse>(mapped);
	}
}