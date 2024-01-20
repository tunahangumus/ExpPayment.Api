using ExpPayment.Base.Response;
using ExpPayment.Schema;
using MediatR;

namespace ExpPayment.Business.Cqrs;

public record AdminPaymentDemandApproveCommand(int paymentDemandId,int userId,AdminPaymentDemandApproval Model) : IRequest<ApiResponse>;
public record AdminPaymentDemandRejectCommand(int paymentDemandId,int userId,AdminPaymentDemandApproval Model) : IRequest<ApiResponse>;
public record AdminPaymentDemandEditCommand(int paymentDemandId,int userId,string description) : IRequest<ApiResponse>;
public record AdminDeletePaymentDemandCommand(int paymentDemandId, int userId) : IRequest<ApiResponse>;




public record AdminGetAllActivePaymentDemandQuery() : IRequest<ApiResponse<List<PaymentDemandResponse>>>;
public record AdminGetAllPassivePaymentDemandQuery() : IRequest<ApiResponse<List<PaymentDemandResponse>>>;
public record AdminGetAllActivePaymentDemandByPersonelQuery(int userId) : IRequest<ApiResponse<List<PaymentDemandResponse>>>;