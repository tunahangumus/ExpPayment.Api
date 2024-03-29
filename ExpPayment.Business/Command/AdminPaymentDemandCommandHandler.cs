﻿using AutoMapper;
using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data.Entity;
using ExpPayment.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpPayment.Business.Command;

public class AdminPaymentDemandCommandHandler :
	IRequestHandler<AdminPaymentDemandApproveCommand, ApiResponse>,
	IRequestHandler<AdminPaymentDemandRejectCommand, ApiResponse>,
	IRequestHandler<AdminDeletePaymentDemandCommand, ApiResponse>,
	IRequestHandler<AdminPaymentDemandEditCommand, ApiResponse>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;

	public AdminPaymentDemandCommandHandler(ExpPaymentDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse> Handle(AdminPaymentDemandApproveCommand request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<PaymentDemand>().Where(x => x.Id == request.paymentDemandId && x.IsActive==true).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			//In here there must be a request to Bank API to complete payment. By Using first name, last name, IBAN of the 
			//User we can make a api call to make EFT. In that situation RabbitMQ can be used.
			entity.IsActive = false;
			entity.IsApproved = true;
			entity.Description = request.Model.Description;
			entity.UpdateDate = DateTime.UtcNow;
			entity.UpdateUserId = request.userId;
			await dbContext.SaveChangesAsync();
			return new ApiResponse("Payment demand successfully approved.");
		}
		else
		{
			return new ApiResponse("Either the payment demand is not valid or payment process has been completed.");
		}
	}

	public async Task<ApiResponse> Handle(AdminPaymentDemandRejectCommand request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<PaymentDemand>().Where(x => x.Id == request.paymentDemandId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			entity.IsActive = false;
			entity.IsApproved = false;
			entity.Description = request.Model.Description;
			entity.UpdateDate = DateTime.UtcNow;
			entity.UpdateUserId = request.userId;
			await dbContext.SaveChangesAsync();
			return new ApiResponse("Payment demand successfully rejected.");
		}
		else
		{
			return new ApiResponse("Either the payment demand is not valid or payment process has been completed.");
		}
	}

	public async Task<ApiResponse> Handle(AdminPaymentDemandEditCommand request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<PaymentDemand>().Where(x => x.Id == request.paymentDemandId && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			entity.UpdateDate = DateTime.UtcNow;
			entity.UpdateUserId = request.userId;
			entity.Description = request.description;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse("Payment demand successfully updated.");
		}
		else
		{
			return new ApiResponse("There is no such Payment Demand recor");
		}
	}

	public async Task<ApiResponse> Handle(AdminDeletePaymentDemandCommand request, CancellationToken cancellationToken)
	{
		var entity = await dbContext.Set<PaymentDemand>().Where(x => x.Id == request.paymentDemandId).FirstOrDefaultAsync(cancellationToken);
		if (entity != null)
		{
			//used hard delete here because expense id must be unique.
			dbContext.Remove(entity);
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse("Payment demand successfully cancelled.");
		}
		else
		{
			return new ApiResponse("There is no such PaymentDemand record.");
		}
	}
}