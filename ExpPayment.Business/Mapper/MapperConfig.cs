using AutoMapper;
using ExpPayment.Data.Entity;
using ExpPayment.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Mapper;

public class MapperConfig : Profile
{
	public MapperConfig()
	{
		CreateMap<ExpenseRequest, Expense>();
		CreateMap<Expense, ExpenseResponse>();

		CreateMap<Tuple<Expense, PaymentDemand>, PaymentDemandResponse>()
			.ForMember(d => d.IsApproved, opt => opt.MapFrom(s => s.Item2.IsApproved))
			.ForMember(d => d.Title, opt => opt.MapFrom(s => s.Item2.Title))
			.ForMember(d => d.Description, opt => opt.MapFrom(s => s.Item2.Description))
			.ForMember(d => d.Date, opt => opt.MapFrom(s => s.Item2.Date))
			.ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Item1.Amount))
			.ForMember(d => d.City, opt => opt.MapFrom(s => s.Item1.City))
			.ForMember(d => d.Country, opt => opt.MapFrom(s => s.Item1.Country))
			.ForMember(d => d.Id, opt => opt.MapFrom(s => s.Item2.Id));

		CreateMap<PaymentTypeRequest, PaymentType>();
		CreateMap<PaymentType, PaymentTypeResponse>();

		CreateMap<PaymentDemandRequest, PaymentDemand>();
		CreateMap<PaymentDemand, PaymentDemandResponse>()
			.ForMember(d=>d.Amount, opt=>opt.MapFrom(s=>s.Expense.Amount));

		CreateMap<PaymentCategoryRequest, PaymentCategory>();
		CreateMap<PaymentCategory, PaymentCategoryResponse>();

		CreateMap<ApplicationUserRequest, ApplicationUser>();
		CreateMap<ApplicationUser, ApplicationUserResponse>();
	}
}