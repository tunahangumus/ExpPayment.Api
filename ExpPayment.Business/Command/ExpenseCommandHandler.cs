using AutoMapper;
using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data;
using ExpPayment.Data.Entity;
using ExpPayment.Schema;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ExpPayment.Business.Command;

public class ExpenseCommandHandler :
	IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
	IRequestHandler<UpdateExpenseCommand, ApiResponse>,
	IRequestHandler<DeleteExpenseCommand, ApiResponse>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;

	public ExpenseCommandHandler(ExpPaymentDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
	{
		var entity = mapper.Map<ExpenseRequest, Expense>(request.Model);
		entity.PersonelId = request.userId;
		entity.IsActive = true;
		var entityResult = await dbContext.AddAsync(entity, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);

		var mapped = mapper.Map<Expense, ExpenseResponse>(entityResult.Entity);
		return new ApiResponse<ExpenseResponse>(mapped);
	}

	public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<Expense>().Where(x => x.PersonelId == request.userId && x.Id == request.expenseId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if(entity != null)
		{
			entity.City = request.Model.City;
			entity.Country = request.Model.Country;
			entity.Description = request.Model.Description;
			entity.Amount = request.Model.Amount;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}
		else
		{
			return new ApiResponse("Either there is no such expense record or the selected expense is not belong to this user");
		}
	}

	public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<Expense>().Where(x => x.PersonelId == request.userId && x.Id == request.expenseId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			entity.IsActive = false;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}
		else
		{
			return new ApiResponse("Either there is no such expense record or the selected expense is not belong to this user");
		}
	}
}