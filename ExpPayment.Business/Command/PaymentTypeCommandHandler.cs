using AutoMapper;
using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data.Entity;
using ExpPayment.Data;
using ExpPayment.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpPayment.Data.Migrations;
using Microsoft.EntityFrameworkCore;

namespace ExpPayment.Business.Command;
public class PaymentTypeCommandHandler :
	IRequestHandler<CreatePaymentTypeCommand, ApiResponse<PaymentTypeResponse>>,
	IRequestHandler<UpdatePaymentTypeCommand, ApiResponse>,
	IRequestHandler<DeletePaymentTypeCommand, ApiResponse>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;

	public PaymentTypeCommandHandler(ExpPaymentDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse<PaymentTypeResponse>> Handle(CreatePaymentTypeCommand request, CancellationToken cancellationToken)
	{
		var checkEntity = await dbContext.Set<PaymentType>().Where(x => x.Name == request.Model.Name && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (checkEntity != null)
		{
			return new ApiResponse<PaymentTypeResponse>($"There is already a Payment Type named {request.Model.Name}");
		}
		var entity = mapper.Map<PaymentTypeRequest, PaymentType>(request.Model);
		entity.InsertDate = DateTime.UtcNow;
		entity.InsertUserId = request.userId;
		entity.IsActive = true;
		var entityResult = await dbContext.AddAsync(entity, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);

		var mapped = mapper.Map<PaymentType, PaymentTypeResponse>(entityResult.Entity);
		return new ApiResponse<PaymentTypeResponse>(mapped);
	}

	public async Task<ApiResponse> Handle(UpdatePaymentTypeCommand request, CancellationToken cancellationToken)
	{
		var checkEntity = await dbContext.Set<PaymentType>().Where(x => x.Name == request.Model.Name && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (checkEntity != null)
		{
			return new ApiResponse($"There is already a Payment Type named {request.Model.Name}");
		}
		var entity = await dbContext.Set<PaymentType>().Where(x => x.Id == request.PaymentTypeId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			entity.Name = request.Model.Name;
			entity.UpdateDate= DateTime.UtcNow;
			entity.UpdateUserId = request.userId;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse("PaymentType successfully updated.");
		}
		else
		{
			return new ApiResponse("Either there is no such PaymentType record or the selected PaymentType is not belong to this user");
		}
	}

	public async Task<ApiResponse> Handle(DeletePaymentTypeCommand request, CancellationToken cancellationToken)
	{
		var paymentDemands = await dbContext.Set<PaymentDemand>().Where(x => x.Id == request.PaymentTypeId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (paymentDemands != null)
		{
			return new ApiResponse("It is danngerous to delete that Payment Type because it is used by a Payment Demand.");
		}
		var entity = await dbContext.Set<PaymentType>().Where(x =>x.Id == request.PaymentTypeId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			entity.IsActive = false;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse("PaymentType successfully deleted.");
		}
		else
		{
			return new ApiResponse("There is no such PaymentType record.");
		}
	}
}