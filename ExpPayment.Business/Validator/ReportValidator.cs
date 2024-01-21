using ExpPayment.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Validator;

public class CreateReportValidator : AbstractValidator<ReportRequest>
{
	public CreateReportValidator()
	{
		RuleFor(x => x.Period).NotEmpty().Must(BeValidPeriod).WithMessage("Period must be either Daily,Weekly or Monthly");
	}
	private bool BeValidPeriod(string period)
	{
		return period == "Daily" || period == "Weekly" || period == "Monthly";
	}
}