using ExpPayment.Base.Response;
using ExpPayment.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Cqrs;



public record GetPersonelTransactionQuery(int id) : IRequest<ApiResponse<List<PaymentDemandResponse>>>;


public record GetCompanyAllPaymentQuery(int id) : IRequest<ApiResponse<List<PaymentDemandResponse>>>;
public record GetExpenseVolumeByPersonelIdQuery(int id) : IRequest<ApiResponse<List<PaymentDemandResponse>>>;
public record GetAllRejectedPaymentDemandQuery(int id) : IRequest<ApiResponse<List<PaymentDemandResponse>>>;
public record GetAllApprovedPaymentDemandQuery(int id) : IRequest<ApiResponse<List<PaymentDemandResponse>>>;