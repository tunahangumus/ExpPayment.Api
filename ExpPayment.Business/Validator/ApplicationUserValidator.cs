using ExpPayment.Data.Entity;
using ExpPayment.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Validator;

public class CreateApplicationUserValidator : AbstractValidator<ApplicationUserRequest>
{
	public CreateApplicationUserValidator()
	{
		RuleFor(x => x.UserName).NotEmpty().MaximumLength(20);
		RuleFor(x => x.IBAN).NotEmpty().MaximumLength(34);
		RuleFor(x => x.Password).NotEmpty().MaximumLength(250);
		RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
		RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.Role).NotEmpty().Must(beValidRole).WithMessage("Role must be either personel or admin").MaximumLength(30);
	}
	private bool beValidRole(string role)
	{
		return role == "personel" || role == "admin";
	}
}