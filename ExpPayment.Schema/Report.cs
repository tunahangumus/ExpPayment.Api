using ExpPayment.Base.Schema;

namespace ExpPayment.Schema;

public class Report: BaseRequest
{
	public string Period { get; set; }
}

public class ExpenseDetails
{
	public string PaymentType { get; set; }
	public int Amount { get; set; }
}
public class CompanyReportResponse: BaseResponse
{
	public DateTime Date { get; set; }
	public decimal TotalPayment { get; set; }
	public List<ExpenseDetails> Expenses { get; set; }
}


public class CompanyExpenseReportResponse: BaseResponse
{
	public DateTime Date { get; set; }
}

public class CompanyExpenseByPersonelReportResponse: BaseResponse
{
	public DateTime Date { get; set; }
}

public class PersonelReportResponse : BaseResponse
{
	public DateTime Date { get; set; }
	public string PaymentCategory { get; set; }
	public string Description { get; set; }
	public bool IsApproved { get; set; }
}