using System.Collections.Generic;
using FinanceTrack.Models;

namespace FinanceTrack.Interfaces
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAllTransactions();
        Transaction GetTransactionById(int id);
        void AddTransaction(Transaction transaction);
        void UpdateTransactionStatus(int id, string targetStatus);
        
        // Specific queries for the dashboard and views
        decimal GetTotalBalance();
        decimal GetNetProfit();
        decimal GetTotalAssets();
        decimal GetTotalLiabilities();
        IEnumerable<Transaction> GetRecentTransactions(int count = 10);
    }
}
