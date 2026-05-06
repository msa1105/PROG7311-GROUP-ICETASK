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

            // ── User configuration ──────────────────────────────────────────
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.Email)
                      .IsUnique()
                      .HasDatabaseName("IX_Users_Email");

                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);
                entity.Property(u => u.Role).IsRequired().HasMaxLength(50).HasDefaultValue("Staff");
            });

            // ── Transaction configuration ────────────────────────────────────
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Amount)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.Property(t => t.Type).IsRequired().HasMaxLength(50);
                entity.Property(t => t.Status).IsRequired().HasMaxLength(50).HasDefaultValue("Pending");
                entity.Property(t => t.Category).IsRequired().HasMaxLength(100);
                entity.Property(t => t.EntityName).HasMaxLength(200);

                // Index on Date for fast ledger/date-range queries
                entity.HasIndex(t => t.Date)
                      .HasDatabaseName("IX_Transactions_Date");

                // Index on Type for fast category-filter queries
                entity.HasIndex(t => t.Type)
                      .HasDatabaseName("IX_Transactions_Type");

                // Relationship: Transaction → User (optional FK so seeded
                // transactions that predate any real user are still valid)
                entity.HasOne(t => t.User)
                      .WithMany(u => u.Transactions)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ── Audit column configuration ───────────────────────────────────
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.CreatedAt).IsRequired();
                entity.Property(u => u.UpdatedAt).IsRequired();
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(t => t.Description).HasMaxLength(500);
                entity.Property(t => t.CreatedAt).IsRequired();
                entity.Property(t => t.UpdatedAt).IsRequired();
            });

            // Seed data has been moved to DbInitializer.SeedData() so that
            // runtime-relative dates (e.g. DateTime.Today) are not baked into
            // the migration snapshot, which would cause migrations to regenerate
            // on every build.
        }
    }
}
