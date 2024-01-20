using ExpPayment.Base.Schema;


namespace ExpPayment.Schema;

public class AdminPaymentDemandApproval: BaseRequest
{
	public bool IsApproved { get; set; }
	public string Description { get; set; }
}
