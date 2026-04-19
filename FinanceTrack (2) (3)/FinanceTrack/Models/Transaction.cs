using System;

namespace FinanceTrack.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // Income, Expense, Asset, Liability
        public string Status { get; set; } // Paid, Pending
        public string Category { get; set; }
        public string EntityName { get; set; } 
        public DateTime? DueDate { get; set; }
    }
}
