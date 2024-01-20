using ExpPayment.Base.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Data.Entity;

public class Invoice : BaseEntity
{
	public string BusinessName { get; set; }
	public DateTime PurchaseDate { get; set; }
	public string InvoiceNumber { get; set; }
	public string BillingAddress { get; set; }
	public decimal TotalAmount { get; set; }
}
public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
	public void Configure(EntityTypeBuilder<Invoice> builder)
	{
		builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
		builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
		builder.Property(x => x.BusinessName).IsRequired(true).HasMaxLength(30);
		builder.Property(x => x.InvoiceNumber).IsRequired(true).HasMaxLength(100);
		builder.Property(x => x.BillingAddress).IsRequired(true).HasMaxLength(150);

		builder.Property(x => x.InsertDate).IsRequired(true);
		builder.Property(x => x.InsertUserId).IsRequired(true);
		builder.Property(x => x.UpdateDate).IsRequired(false);
		builder.Property(x => x.UpdateUserId).IsRequired(false);

	}
}