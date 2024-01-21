using ExpPayment.Base.Schema;

namespace ExpPayment.Schema;

public class ReportRequest: BaseRequest
{
	public string Period { get; set; }
}



public class PersonelExpenseReport : BaseResponse
{
	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public bool IsApproved { get; set; }
	public string Name { get; set; }
}

public class CompanyPaymentReport : BaseResponse
{
	public decimal Amount { get; set; }
	public string Name { get; set; }
}

public class CompanyExpenseByPersonel : BaseResponse
{
	public int PersonelId { get; set; }
	public decimal Amount { get; set; }
}

public class CompanyPaymentDemandReport : BaseResponse
{
	public bool IsApproved { get; set; }
	public decimal Amount { get; set; }
}