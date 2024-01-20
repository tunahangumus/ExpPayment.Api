﻿using AutoMapper;
using Dapper;
using ExpPayment.Base.Dapper;
using ExpPayment.Base.Response;
using ExpPayment.Business.Cqrs;
using ExpPayment.Data;
using ExpPayment.Schema;
using MediatR;

namespace ExpPayment.Business.Query;

public class ReportQueryHandler :
	IRequestHandler<GetPersonelTransactionQuery, ApiResponse<List<PersonelExpenseReport>>>,
	IRequestHandler<GetCompanyAllPaymentQuery, ApiResponse<List<CompanyPaymentReport>>>,
	IRequestHandler<GetExpenseByPersonelIdQuery, ApiResponse<List<CompanyExpenseByPersonel>>>,
	IRequestHandler<GetAllPaymentDemandQuery, ApiResponse<List<CompanyPaymentDemandReport>>>
{
	private readonly ExpPaymentDbContext dbContext;
	private readonly IMapper mapper;
	private readonly ISqlConnectionFactory connectionFactory;

	public ReportQueryHandler(ExpPaymentDbContext dbContext, IMapper mapper,ISqlConnectionFactory connectionFactory)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
		this.connectionFactory = connectionFactory;
	}
	public async Task<ApiResponse<List<PersonelExpenseReport>>> Handle(GetPersonelTransactionQuery request, CancellationToken cancellationToken)
	{
		using var connection = connectionFactory.Create();
		var query = @"
			SELECT e.""Amount"",e.""Date"",pc.""Name"",pd.""IsApproved"" FROM ""PaymentDemands"" pd
			JOIN ""PaymentCategories"" pc ON pc.""Id"" = pd.""PaymentCategoryId""
			JOIN ""Expenses"" e ON e.""Id"" = pd.""ExpenseId""
			WHERE pd.""InsertUserId"" = @UserId;";
		var parameters = new { UserId = request.userId };
		var personelExpenseReports = await connection.QueryAsync<PersonelExpenseReport>(
			sql: query,
			param: parameters
		);

		return new ApiResponse<List<PersonelExpenseReport>>(personelExpenseReports.ToList());
	}

	public async Task<ApiResponse<List<CompanyPaymentReport>>> Handle(GetCompanyAllPaymentQuery request, CancellationToken cancellationToken)
	{
		using var connection = connectionFactory.Create();
		var query = @"SELECT SUM(e.""Amount"") as Amount, pc.""Name""
					FROM ""PaymentDemands"" pd
					JOIN ""PaymentCategories"" pc ON pc.""Id"" = pd.""PaymentCategoryId""
					JOIN ""Expenses"" e ON e.""Id"" = pd.""ExpenseId""
					WHERE pd.""IsApproved"" = true
					GROUP BY pc.""Name"";";

		var companyreports = await connection.QueryAsync<CompanyPaymentReport>(
			sql: query
		);

		return new ApiResponse<List<CompanyPaymentReport>>(companyreports.ToList());
	}

	public async Task<ApiResponse<List<CompanyExpenseByPersonel>>> Handle(GetExpenseByPersonelIdQuery request, CancellationToken cancellationToken)
	{
		using var connection = connectionFactory.Create();
		var query = @"SELECT SUM(e.""Amount"") as Amount, e.""PersonelId""
						FROM ""Expenses"" e
						GROUP BY e.""PersonelId"";";

		var expenseByPersonal = await connection.QueryAsync<CompanyExpenseByPersonel>(
			sql: query
		);
		return new ApiResponse<List<CompanyExpenseByPersonel>>(expenseByPersonal.ToList());
	}

	public async Task<ApiResponse<List<CompanyPaymentDemandReport>>> Handle(GetAllPaymentDemandQuery request, CancellationToken cancellationToken)
	{
		using var connection = connectionFactory.Create();
		var query = @"SELECT SUM(e.""Amount"") as Amount, pd.""IsApproved""
						FROM ""PaymentDemands"" pd
						JOIN ""Expenses"" e ON e.""Id"" = pd.""ExpenseId""
						GROUP BY pd.""IsApproved"";";

		var expenseByPersonal = await connection.QueryAsync<CompanyPaymentDemandReport>(
			sql: query
		);
		return new ApiResponse<List<CompanyPaymentDemandReport>>(expenseByPersonal.ToList());
	}
}