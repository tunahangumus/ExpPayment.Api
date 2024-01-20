using ExpPayment.Base.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ExpPayment.Data.Entity;

public class Expense:BaseEntity
{
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public float Amount { get; set; }
	public string City  { get; set; }
	public string Country { get; set; }

	public int PersonelId { get; set; }
	public virtual ApplicationUser Personel { get; set; }
}


public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
	public void Configure(EntityTypeBuilder<Expense> builder)
	{
		builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
		builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
		builder.Property(x => x.Title).IsRequired(true).HasMaxLength(30);

		
		builder.Property(x => x.Date).IsRequired(true);
		builder.Property(x => x.Amount).IsRequired(true);
		builder.Property(x => x.City).IsRequired(true);
		builder.Property(x => x.Country).IsRequired(true);

		builder.Property(x => x.InsertDate).IsRequired(true);
		builder.Property(x => x.InsertUserId).IsRequired(true);
		builder.Property(x => x.UpdateDate).IsRequired(false);
		builder.Property(x => x.UpdateUserId).IsRequired(false);
		builder.Property(x => x.PersonelId).IsRequired(true);

		builder.HasIndex(x => x.PersonelId).IsUnique(false);
	}
}
