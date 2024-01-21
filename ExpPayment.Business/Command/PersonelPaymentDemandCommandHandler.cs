using AutoMapper;
using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data.Entity;
using ExpPayment.Data;
using ExpPayment.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpPayment.Business.Command;
public class PersonelPaymentDemandCommandHandler :
	IRequestHandler<CreatePaymentDemandCommand, ApiResponse>,
	IRequestHandler<UpdatePaymentDemandCommand, ApiResponse>,
	IRequestHandler<DeletePaymentDemandCommand, ApiResponse>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;

	public PersonelPaymentDemandCommandHandler(ExpPaymentDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse> Handle(CreatePaymentDemandCommand request, CancellationToken cancellationToken)
	{
		var list = await dbContext.Set<PaymentDemand>().Where(x => x.ExpenseId == request.Model.ExpenseId).Include(x=>x.Expense).ToListAsync(cancellationToken);
		var expense = await dbContext.Set<Expense>().Where(x => x.Id == request.Model.ExpenseId).FirstOrDefaultAsync(cancellationToken);
		var paymentType = await dbContext.Set<PaymentType>().Where(x => x.Id == request.Model.PaymentTypeId).FirstOrDefaultAsync(cancellationToken);
		var paymentCategory = await dbContext.Set<PaymentCategory>().Where(x => x.Id == request.Model.PaymentCategoryId).FirstOrDefaultAsync(cancellationToken);
		if (expense == null || paymentCategory == null || paymentType == null)
		{
			return new ApiResponse("This payment can not be created. At least one of the following are invalid: ExpenseId,PaymentTypeId,PaymentCategoryId");
		}
		if (!list.Any())
		{
			var entity = mapper.Map<PaymentDemandRequest, PaymentDemand>(request.Model);
			entity.InsertDate = DateTime.UtcNow;
			entity.InsertUserId = request.userId;
			entity.IsApproved = false;
			entity.IsActive = true;
			var entityResult = await dbContext.AddAsync(entity, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse(" ");
		}
		else
		{
			return new ApiResponse("This payment demand is already created.");
		}
		
	}

	public async Task<ApiResponse> Handle(UpdatePaymentDemandCommand request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<PaymentDemand>().Where(x => x.InsertUserId == request.userId && x.Id == request.PaymentDemandId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			entity.Title = request.Model.Title;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse(" ");
		}
		else
		{
			return new ApiResponse("Either there is no such Payment Demand record or the selected Payment Demand is not belong to this user");
		}
	}

	public async Task<ApiResponse> Handle(DeletePaymentDemandCommand request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<PaymentDemand>().Where(x => x.InsertUserId == request.userId && x.Id == request.PaymentDemandId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			//used hard delete here because expense id must be unique.
			dbContext.Remove(entity);
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse("Payment demand successfully cancelled.");
		}
		else
		{
			return new ApiResponse("Either there is no such PaymentDemand record or the selected PaymentDemand is not belong to this user");
		}
	}
}