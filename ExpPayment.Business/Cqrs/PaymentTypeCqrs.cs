using ExpPayment.Base.Response;
using ExpPayment.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Cqrs;

public record CreatePaymentTypeCommand(PaymentTypeRequest Model, int userId) : IRequest<ApiResponse<PaymentTypeResponse>>;
public record UpdatePaymentTypeCommand(int userId, int PaymentTypeId, PaymentTypeRequest Model) : IRequest<ApiResponse>;
public record DeletePaymentTypeCommand(int userId, int PaymentTypeId) : IRequest<ApiResponse>;



public record GetAllPaymentTypeQuery() : IRequest<ApiResponse<List<PaymentTypeResponse>>>;
public record GetPaymentTypeByIdQuery(int PaymentTypeId) : IRequest<ApiResponse<PaymentTypeResponse>>;