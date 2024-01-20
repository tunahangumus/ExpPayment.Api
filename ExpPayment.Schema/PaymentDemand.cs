using ExpPayment.Base.Schema;
using ExpPayment.Data.Entity;

namespace ExpPayment.Schema;

public class PaymentDemandRequest : BaseRequest
{
	public string Title { get; set; }
	public int ExpenseId { get; set; }
	public int PaymentTypeId { get; set; }
	public int PaymentCategoryId { get; set; }
	public Invoice Invoice { get; set; }
}

public class PaymentDemandResponse : BaseResponse
{
	public int Id { get; set; }
	public bool IsApproved { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public float Amount { get; set; }
	public string City { get; set; }
	public string Country { get; set; }
}