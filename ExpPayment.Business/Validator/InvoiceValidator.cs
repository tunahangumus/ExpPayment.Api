using ExpPayment.Data.Entity;
using ExpPayment.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Validator;

public class CreateInvoiceValidator : AbstractValidator<Invoice>
{
	public CreateInvoiceValidator()
	{
		RuleFor(x => x.BusinessName).NotEmpty().MaximumLength(30);
		RuleFor(x => x.PurchaseDate).NotEmpty();
		RuleFor(x => x.InvoiceNumber).NotEmpty().MaximumLength(100);
		RuleFor(x => x.BillingAddress).NotEmpty().MaximumLength(150);
		RuleFor(x => x.TotalAmount).NotEmpty();
	}
}