using ExpPayment.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Validator;

public class CreateExpenseValidator : AbstractValidator<ExpenseRequest>
{
	public CreateExpenseValidator()
	{
		RuleFor(x => x.Title).NotEmpty().MaximumLength(20);
		RuleFor(x => x.Date).NotEmpty().InclusiveBetween(DateTime.UtcNow.Date.AddMonths(-4), DateTime.UtcNow.Date.AddDays(1)).WithMessage("You can enter expenses from a maximum of 4 months ago.");
		RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
		RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
		RuleFor(x => x.City).NotEmpty();
		RuleFor(x => x.Country).NotEmpty();
	}
}