using Microsoft.EntityFrameworkCore;
using FinanceTrack.Models;

namespace FinanceTrack.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data directly into SQLite
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin User", Email = "admin@financetrack.com", Role = "Admin" },
                new User { Id = 2, Name = "Staff Member", Email = "staff@financetrack.com", Role = "Staff" }
            );

            var today = System.DateTime.Today;
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, Date = today.AddMonths(-1), Amount = 28000, Type = "Income", Status = "Paid", Category = "Service Fees", EntityName = "Future Tech", DueDate = today.AddDays(-5) },
                new Transaction { Id = 2, Date = today.AddMonths(-2), Amount = 14500, Type = "Income", Status = "Paid", Category = "Product Sales", EntityName = "New Customer", DueDate = today.AddDays(-12) },
                new Transaction { Id = 3, Date = today.AddMonths(-1).AddDays(-5), Amount = 5000, Type = "Expense", Status = "Paid", Category = "Rent", EntityName = "Property Management Co", DueDate = today.AddMonths(-1) },
                new Transaction { Id = 4, Date = today.AddMonths(-3), Amount = 18000, Type = "Expense", Status = "Paid", Category = "Salaries", EntityName = "Employees", DueDate = today.AddMonths(-3) },
                new Transaction { Id = 5, Date = today, Amount = 12000, Type = "Liability", Status = "Pending", Category = "Credit Line", EntityName = "Business Credit Card", DueDate = today.AddDays(20) },
                new Transaction { Id = 6, Date = today.AddDays(-2), Amount = 830, Type = "Expense", Status = "Pending", Category = "Utilities", EntityName = "Electric Company", DueDate = today.AddDays(5) },
                new Transaction { Id = 7, Date = today.AddDays(15), Amount = 66000, Type = "Receivable", Status = "Pending", Category = "Service Fees", EntityName = "Giant Corp", DueDate = today.AddDays(15) },
                new Transaction { Id = 8, Date = today.AddDays(-10), Amount = 150000, Type = "Liability", Status = "Pending", Category = "Loans", EntityName = "Bank Ltd", DueDate = today.AddDays(300) },
                new Transaction { Id = 9, Date = today.AddMonths(-6), Amount = 170000, Type = "Asset", Status = "Paid", Category = "Equipment", EntityName = "Asset Vendor", DueDate = today.AddMonths(-6) },
                new Transaction { Id = 10, Date = today.AddMonths(-1), Amount = 55000, Type = "Asset", Status = "Paid", Category = "Cash Equivalents", EntityName = "Bank Ltd", DueDate = today.AddMonths(-1) }
            );
        }
    }
}
