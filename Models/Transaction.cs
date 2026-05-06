using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTrack.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        /// <summary>Income | Expense | Asset | Liability | Receivable</summary>
        [Required, MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        /// <summary>Paid | Pending</summary>
        [Required, MaxLength(50)]
        public string Status { get; set; } = "Pending";

        [Required, MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? EntityName { get; set; }

        public DateTime? DueDate { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key — links each transaction to the user who created it
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
