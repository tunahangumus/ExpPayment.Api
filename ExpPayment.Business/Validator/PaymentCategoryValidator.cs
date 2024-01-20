using ExpPayment.Schema;
using FluentValidation;

namespace ExpPayment.Business.Validator;

public class CreatePaymentCategoryValidator : AbstractValidator<PaymentCategoryRequest>
{
	public CreatePaymentCategoryValidator()
	{
		RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
	}
}