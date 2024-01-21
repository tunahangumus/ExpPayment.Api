using ExpPayment.Base.Response;
using ExpPayment.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Cqrs;



public record GetPersonelTransactionQuery(int userId) : IRequest<ApiResponse<List<PersonelExpenseReport>>>;


public record GetCompanyAllPaymentQuery(ReportRequest Model) : IRequest<ApiResponse<List<CompanyPaymentReport>>>;
public record GetExpenseByPersonelIdQuery(ReportRequest Model) : IRequest<ApiResponse<List<CompanyExpenseByPersonel>>>;
public record GetAllPaymentDemandQuery(ReportRequest Model) : IRequest<ApiResponse<List<CompanyPaymentDemandReport>>>;