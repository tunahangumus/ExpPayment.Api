using ExpPayment.Base.Response;
using ExpPayment.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Cqrs;

public record CreatePaymentCategoryCommand(PaymentCategoryRequest Model, int userId) : IRequest<ApiResponse<PaymentCategoryResponse>>;
public record UpdatePaymentCategoryCommand(int userId, int PaymentCategoryId, PaymentCategoryRequest Model) : IRequest<ApiResponse>;
public record DeletePaymentCategoryCommand(int userId, int PaymentCategoryId) : IRequest<ApiResponse>;



public record GetAllPaymentCategoryQuery() : IRequest<ApiResponse<List<PaymentCategoryResponse>>>;
public record GetPaymentCategoryByIdQuery(int PaymentCategoryId) : IRequest<ApiResponse<PaymentCategoryResponse>>;