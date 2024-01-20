using ExpPayment.Base.Response;
using ExpPayment.Schema;
using MediatR;

namespace ExpPayment.Business.Cqrs;

public record CreatePaymentDemandCommand(PaymentDemandRequest Model, int userId) : IRequest<ApiResponse>;
public record UpdatePaymentDemandCommand(int userId, int PaymentDemandId, PaymentDemandRequest Model) : IRequest<ApiResponse>;
public record DeletePaymentDemandCommand(int userId, int PaymentDemandId) : IRequest<ApiResponse>;



public record GetAllActivePaymentDemandQuery(int id) : IRequest<ApiResponse<List<PaymentDemandResponse>>>;
public record GetAllPassivePaymentDemandQuery(int id) : IRequest<ApiResponse<List<PaymentDemandResponse>>>;
public record GetPaymentDemandByIdQuery(int userId, int PaymentDemandId) : IRequest<ApiResponse<PaymentDemandResponse>>;
