using ExpPayment.Base.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Data.Entity;

public class PaymentCategory: BaseEntity
{
	public string Name { get; set; }
}

public class PaymentCategoryConfiguration : IEntityTypeConfiguration<PaymentCategory>
{
	public void Configure(EntityTypeBuilder<PaymentCategory> builder)
	{
		builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
		builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
		builder.Property(x => x.Name).IsRequired(true).HasMaxLength(20);

		builder.Property(x => x.InsertDate).IsRequired(true);
		builder.Property(x => x.InsertUserId).IsRequired(true);
		builder.Property(x => x.UpdateDate).IsRequired(false);
		builder.Property(x => x.UpdateUserId).IsRequired(false);
	}
}
