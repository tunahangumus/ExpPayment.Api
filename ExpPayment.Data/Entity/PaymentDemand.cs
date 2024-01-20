using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ExpPayment.Base.Entity;

namespace ExpPayment.Data.Entity;

public class PaymentDemand: BaseEntity
{
	public bool IsApproved { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public int ExpenseId { get; set; }
	public virtual Expense Expense { get; set; }

	public int PaymentTypeId { get; set; }
	public virtual PaymentType PaymentType { get; set; }

	public int PaymentCategoryId { get; set; }
	public  virtual PaymentCategory PaymentCategory { get; set; }

	public int InvoiceId { get; set; }
	public virtual Invoice Invoice { get; set; }

}
public class PaymentDemandConfiguration : IEntityTypeConfiguration<PaymentDemand>
{
	public void Configure(EntityTypeBuilder<PaymentDemand> builder)
	{
		builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
		builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
		builder.Property(x => x.Date).IsRequired(true);
		builder.Property(x => x.Title).IsRequired(true).HasMaxLength(30);
		builder.Property(x => x.Description).IsRequired(true).HasMaxLength(90).HasDefaultValue(" ");

		builder.Property(x => x.ExpenseId).IsRequired(true);
		builder.Property(x => x.PaymentTypeId).IsRequired(true);
		builder.Property(x => x.PaymentCategoryId).IsRequired(true);
		builder.Property(x => x.InvoiceId).IsRequired(true);

		builder.Property(x => x.InsertDate).IsRequired(true);
		builder.Property(x => x.InsertUserId).IsRequired(true);
		builder.Property(x => x.UpdateDate).IsRequired(false);
		builder.Property(x => x.UpdateUserId).IsRequired(false);

		builder.HasIndex(x => x.ExpenseId).IsUnique(true);
	}
}
