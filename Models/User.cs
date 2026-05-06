using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceTrack.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Role { get; set; } = "Staff"; // Admin or Staff

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property — one user owns many transactions
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
