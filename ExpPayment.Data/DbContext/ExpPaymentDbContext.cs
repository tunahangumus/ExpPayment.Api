using ExpPayment.Data.Entity;
using Microsoft.EntityFrameworkCore;
namespace ExpPayment.Data;

public class ExpPaymentDbContext : DbContext
{
	public ExpPaymentDbContext(DbContextOptions<ExpPaymentDbContext> options) : base(options)
	{

	}

	// dbset 
	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	public DbSet<Expense> Expenses { get; set; }
	public DbSet<Invoice> Invoices { get; set; }
	public DbSet<PaymentCategory> PaymentCategories { get; set; }
	public DbSet<PaymentDemand> PaymentDemands { get; set; }
	public DbSet<PaymentType> PaymentTypes { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
		modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
		modelBuilder.ApplyConfiguration(new PaymentCategoryConfiguration());
		modelBuilder.ApplyConfiguration(new PaymentDemandConfiguration());
		modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());
		modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
		base.OnModelCreating(modelBuilder);
	}

}