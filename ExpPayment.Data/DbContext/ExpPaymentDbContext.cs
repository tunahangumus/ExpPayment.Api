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

		modelBuilder.Entity<ApplicationUser>().HasData(
			   new ApplicationUser
			   {
				   Id = 1,
				   UserName = "admin",
				   Password = "0c909a141f1f2c0a1cb602b0b2d7d050",
				   FirstName = "Admin",
				   LastName = "User",
				   Email = "admin@example.com",
				   IBAN = "AdminIBAN",
				   Role = "admin",
				   LastActivityDate = DateTime.UtcNow,
				   PasswordRetryCount = 0,
				   Status = 1 
			   },
			   new ApplicationUser
			   {
				   Id = 2,
				   UserName = "personel",
				   Password = "307802b31f1beecbbca17bcc4d6964d2",
				   FirstName = "Regular",
				   LastName = "User",
				   Email = "user@example.com",
				   IBAN = "UserIBAN",
				   Role = "personel",
				   LastActivityDate = DateTime.UtcNow,
				   PasswordRetryCount = 0,
				   Status = 1 
			   }
		   );
		base.OnModelCreating(modelBuilder);
	}

}