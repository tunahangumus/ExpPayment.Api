using ExpPayment.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Validator;


public class CreatePaymentTypeValidator : AbstractValidator<PaymentTypeRequest>
{
	public CreatePaymentTypeValidator()
	{
		RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
	}
}