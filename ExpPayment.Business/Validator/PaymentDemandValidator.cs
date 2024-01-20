using ExpPayment.Schema;
using FluentValidation;

namespace ExpPayment.Business.Validator;

public class CreatePaymentDemandValidator : AbstractValidator<PaymentDemandRequest>
{
	public CreatePaymentDemandValidator()
	{
		RuleFor(x => x.Title).NotEmpty().MaximumLength(20);
		RuleFor(x => x.ExpenseId).NotEmpty();
		RuleFor(x => x.PaymentTypeId).NotEmpty();
		RuleFor(x => x.PaymentCategoryId).NotEmpty();
		RuleFor(x => x.Invoice).NotEmpty();

		RuleFor(x => x.Invoice).SetValidator(new CreateInvoiceValidator());
	}
}