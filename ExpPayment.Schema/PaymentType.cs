using ExpPayment.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Schema;


public class PaymentTypeRequest : BaseRequest
{
	public string Name { get; set; }
}

public class PaymentTypeResponse : BaseResponse
{
	public int Id { get; set; }
	public bool IsActive { get; set; }
	public int InsertUserId { get; set; }
	public DateTime InsertDate { get; set; }
	public int? UpdateUserId { get; set; }
	public DateTime? UpdateDate { get; set; }
	public string Name { get; set; }
}