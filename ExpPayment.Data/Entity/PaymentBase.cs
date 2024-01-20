using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Data.Entity;

public class PaymentBase
{
	public int Id { get; set; }
	public string Title { get; set; }
	public bool IsActive { get; set; }


}
