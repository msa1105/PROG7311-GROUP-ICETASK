using FinanceTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrack.Data
{
    /// <summary>
    /// Ensures the database schema is up-to-date and the required seed data
    /// exists. Called once at application startup from Program.cs.
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Apply any pending migrations (creates the DB file if it doesn't exist).
            // This replaces EnsureCreated() so that schema changes are tracked and
            // applied correctly across environments without data loss.
            context.Database.Migrate();

            SeedData(context);
        }

        /// <summary>
        /// Seeds required users and sample transactions if the tables are empty.
        /// Seeding is done here (not in HasData) so that runtime-relative dates
        /// do not pollute the migration snapshot.
        /// </summary>
        private static void SeedData(ApplicationDbContext context)
        {
            var now = DateTime.UtcNow;

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        Name = "Admin User",
                        Email = "admin@financetrack.com",
                        Role = "Admin",
                        PasswordHash = HashPassword("admin123"),
                        CreatedAt = now,
                        UpdatedAt = now
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Staff Member",
                        Email = "staff@financetrack.com",
                        Role = "Staff",
                        PasswordHash = HashPassword("staff123"),
                        CreatedAt = now,
                        UpdatedAt = now
                    }
                );
                context.SaveChanges();
            }

            if (!context.Transactions.Any())
            {
                var today = DateTime.Today;
                context.Transactions.AddRange(
                    new Transaction { Date = today.AddMonths(-1),          Amount = 28000,  Type = "Income",     Status = "Paid",    Category = "Service Fees",    EntityName = "Future Tech",            DueDate = today.AddDays(-5),   UserId = 1, CreatedAt = now, UpdatedAt = now },
                    new Transaction { Date = today.AddMonths(-2),          Amount = 14500,  Type = "Income",     Status = "Paid",    Category = "Product Sales",   EntityName = "New Customer",           DueDate = today.AddDays(-12),  UserId = 1, CreatedAt = now, UpdatedAt = now },
                    new Transaction { Date = today.AddMonths(-1).AddDays(-5), Amount = 5000, Type = "Expense",   Status = "Paid",    Category = "Rent",            EntityName = "Property Management Co", DueDate = today.AddMonths(-1), UserId = 1, CreatedAt = now, UpdatedAt = now },
                    new Transaction { Date = today.AddMonths(-3),          Amount = 18000,  Type = "Expense",    Status = "Paid",    Category = "Salaries",        EntityName = "Employees",              DueDate = today.AddMonths(-3), UserId = 1, CreatedAt = now, UpdatedAt = now },
                    new Transaction { Date = today,                        Amount = 12000,  Type = "Liability",  Status = "Pending", Category = "Credit Line",     EntityName = "Business Credit Card",   DueDate = today.AddDays(20),   UserId = 1, CreatedAt = now, UpdatedAt = now },
                    new Transaction { Date = today.AddDays(-2),            Amount = 830,    Type = "Expense",    Status = "Pending", Category = "Utilities",       EntityName = "Electric Company",       DueDate = today.AddDays(5),    UserId = 1, CreatedAt = now, UpdatedAt = now },
                    new Transaction { Date = today.AddDays(15),            Amount = 66000,  Type = "Receivable", Status = "Pending", Category = "Service Fees",    EntityName = "Giant Corp",             DueDate = today.AddDays(15),   UserId = 1, CreatedAt = now, UpdatedAt = now },
                    new Transaction { Date = today.AddDays(-10),           Amount = 150000, Type = "Liability",  Status = "Pending", Category = "Loans",           EntityName = "Bank Ltd",               DueDate = today.AddDays(300),  UserId = 1, CreatedAt = now, UpdatedAt = now },
                    new Transaction { Date = today.AddMonths(-6),          Amount = 170000, Type = "Asset",      Status = "Paid",    Category = "Equipment",       EntityName = "Asset Vendor",           DueDate = today.AddMonths(-6), UserId = 1, CreatedAt = now, UpdatedAt = now },
                    new Transaction { Date = today.AddMonths(-1),          Amount = 55000,  Type = "Asset",      Status = "Paid",    Category = "Cash Equivalents",EntityName = "Bank Ltd",               DueDate = today.AddMonths(-1), UserId = 1, CreatedAt = now, UpdatedAt = now }
                );
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Produces a SHA-256 hex hash of <paramref name="input"/>.
        /// Replace with BCrypt or Argon2 for production-grade security.
        /// </summary>
        public static string HashPassword(string input)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }

        /// <summary>
        /// Verifies a plain-text password against a stored hash.
        /// </summary>
        public static bool VerifyPassword(string plainText, string storedHash)
            => HashPassword(plainText) == storedHash;
    }
}

