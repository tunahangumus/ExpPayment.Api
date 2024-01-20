using ExpPayment.Base.Response;
using ExpPayment.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Cqrs;

public record CreateExpenseCommand(ExpenseRequest Model,int userId) : IRequest<ApiResponse<ExpenseResponse>>;
public record UpdateExpenseCommand(int userId,int expenseId,ExpenseRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenseCommand(int userId,int expenseId) : IRequest<ApiResponse>;



public record GetAllExpenseQuery(int id) : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetExpenseByIdQuery(int userId,int expenseId) : IRequest<ApiResponse<ExpenseResponse>>;