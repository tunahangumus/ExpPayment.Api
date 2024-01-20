using ExpPayment.Base.Schema;

namespace ExpPayment.Schema;

public class ExpenseRequest : BaseRequest
{
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public float Amount { get; set; }
	public string City { get; set; }
	public string Country { get; set; }
}

public class ExpenseResponse : BaseResponse
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public float Amount { get; set; }
	public string City { get; set; }
	public string Country { get; set; }
}