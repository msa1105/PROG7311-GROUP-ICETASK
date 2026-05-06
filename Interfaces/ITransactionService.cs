using System.Collections.Generic;
using FinanceTrack.Models;

namespace FinanceTrack.Interfaces
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAllTransactions();
        Transaction? GetTransactionById(int id);
        void AddTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void UpdateTransactionStatus(int id, string targetStatus);
        void DeleteTransaction(int id);

        // Filtered query used by Ledger, Expenses, Income, ReceivablePayable views
        IEnumerable<Transaction> GetFilteredTransactions(string? type, string? status, DateTime? startDate, DateTime? endDate);

        // Aggregates for the dashboard and summary views
        decimal GetTotalBalance();
        decimal GetNetProfit();
        decimal GetTotalAssets();
        decimal GetTotalLiabilities();
        IEnumerable<Transaction> GetRecentTransactions(int count = 10);
    }
}
