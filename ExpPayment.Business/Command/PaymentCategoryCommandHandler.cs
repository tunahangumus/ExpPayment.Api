using AutoMapper;
using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data.Entity;
using ExpPayment.Data;
using ExpPayment.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpPayment.Business.Command;

public class PaymentCategoryCommandHandler :
	IRequestHandler<CreatePaymentCategoryCommand, ApiResponse<PaymentCategoryResponse>>,
	IRequestHandler<UpdatePaymentCategoryCommand, ApiResponse>,
	IRequestHandler<DeletePaymentCategoryCommand, ApiResponse>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;

	public PaymentCategoryCommandHandler(ExpPaymentDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse<PaymentCategoryResponse>> Handle(CreatePaymentCategoryCommand request, CancellationToken cancellationToken)
	{
		var checkEntity = await dbContext.Set<PaymentCategory>().Where(x => x.Name == request.Model.Name && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if(checkEntity != null)
		{
			return new ApiResponse<PaymentCategoryResponse>($"There is already a category named {request.Model.Name}");
		}
		var entity = mapper.Map<PaymentCategoryRequest, PaymentCategory>(request.Model);
		entity.InsertDate = DateTime.UtcNow;
		entity.InsertUserId = request.userId;
		entity.IsActive = true;
		var entityResult = await dbContext.AddAsync(entity, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);

		var mapped = mapper.Map<PaymentCategory, PaymentCategoryResponse>(entityResult.Entity);
		return new ApiResponse<PaymentCategoryResponse>(mapped);
	}

	public async Task<ApiResponse> Handle(UpdatePaymentCategoryCommand request, CancellationToken cancellationToken)
	{
		var checkEntity = await dbContext.Set<PaymentCategory>().Where(x => x.Name == request.Model.Name && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (checkEntity != null)
		{
			return new ApiResponse($"There is already a category named {request.Model.Name}");
		}
		var entity = await dbContext.Set<PaymentCategory>().Where(x => x.Id == request.PaymentCategoryId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			entity.Name = request.Model.Name;
			entity.UpdateDate = DateTime.UtcNow;
			entity.UpdateUserId = request.userId;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse("PaymentCategory successfully updated.");
		}
		else
		{
			return new ApiResponse("Either there is no such PaymentCategory record or the selected PaymentCategory is not belong to this user");
		}
	}

	public async Task<ApiResponse> Handle(DeletePaymentCategoryCommand request, CancellationToken cancellationToken)
	{
		var paymentDemands = await dbContext.Set<PaymentDemand>().Where(x => x.Id == request.PaymentCategoryId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (paymentDemands != null)
		{
			return new ApiResponse("It is danngerous to delete that category because it is used by a Payment Demand.");
		}
		var entity = await dbContext.Set<PaymentCategory>().Where(x => x.Id == request.PaymentCategoryId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			entity.IsActive = false;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse("PaymentCategory successfully deleted.");
		}
		else
		{
			return new ApiResponse("Either there is no such PaymentCategory record or the selected PaymentCategory is not belong to this user");
		}
	}
}