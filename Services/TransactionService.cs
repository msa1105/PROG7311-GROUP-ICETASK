using System.Collections.Generic;
using System.Linq;
using FinanceTrack.Data;
using FinanceTrack.Interfaces;
using FinanceTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrack.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _context.Transactions.OrderByDescending(t => t.Date).ToList();
        }

        public decimal GetNetProfit()
        {
            var totalIncome = _context.Transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            var totalExpense = _context.Transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
            return totalIncome - totalExpense;
        }

        public IEnumerable<Transaction> GetRecentTransactions(int count = 10)
        {
            return _context.Transactions.OrderByDescending(t => t.Date).Take(count).ToList();
        }

        public decimal GetTotalAssets()
        {
            return _context.Transactions.Where(t => t.Type == "Asset" && t.Status == "Paid").Sum(t => t.Amount);
        }

        public decimal GetTotalBalance()
        {
            var paidIncome = _context.Transactions.Where(t => t.Type == "Income" && t.Status == "Paid").Sum(t => t.Amount);
            var paidExpense = _context.Transactions.Where(t => t.Type == "Expense" && t.Status == "Paid").Sum(t => t.Amount);
            return paidIncome - paidExpense;
        }

        public decimal GetTotalLiabilities()
        {
            return _context.Transactions.Where(t => t.Type == "Liability").Sum(t => t.Amount);
        }

        public Transaction GetTransactionById(int id)
        {
            return _context.Transactions.FirstOrDefault(t => t.Id == id);
        }

        public void UpdateTransactionStatus(int id, string targetStatus)
        {
            var tx = GetTransactionById(id);
            if (tx != null)
            {
                tx.Status = targetStatus;
                _context.SaveChanges();
            }
        }
    }
}

