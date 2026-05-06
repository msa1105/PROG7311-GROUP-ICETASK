using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinanceTrack.Interfaces;
using FinanceTrack.Models;

namespace FinanceTrack.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Mark a transaction as Paid (Receivable / Payable)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkPaid(int id)
        {
            _transactionService.UpdateTransactionStatus(id, "Paid");
            return RedirectToAction(nameof(ReceivablePayable));
        }

        // Ledger with filters
        public IActionResult Ledger(string? type, string? status, DateTime? startDate, DateTime? endDate, string? search)
        {
            var transactions = _transactionService.GetFilteredTransactions(type, status, startDate, endDate);

            if (!string.IsNullOrWhiteSpace(search))
                transactions = transactions.Where(t =>
                    (t.EntityName != null && t.EntityName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    t.Category.Contains(search, StringComparison.OrdinalIgnoreCase));

            ViewBag.Type = type;
            ViewBag.Status = status;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.Search = search;

            return View(transactions);
        }

        // Details
        public IActionResult Details(int id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        // Create Transaction
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
                return View(transaction);

            _transactionService.AddTransaction(transaction);
            return RedirectToAction(nameof(Ledger));
        }

        // Edit Transaction
        public IActionResult Edit(int id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Transaction transaction)
        {
            if (!ModelState.IsValid)
                return View(transaction);

            _transactionService.UpdateTransaction(transaction);
            return RedirectToAction(nameof(Ledger));
        }

        // Delete Transaction
        public IActionResult Delete(int id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _transactionService.DeleteTransaction(id);
            return RedirectToAction(nameof(Ledger));
        }

        // Receivable / Payable
        public IActionResult ReceivablePayable()
        {
            var transactions = _transactionService.GetFilteredTransactions(null, "Pending", null, null);
            return View(transactions);
        }

        // Expenses view
        public IActionResult Expenses()
        {
            var transactions = _transactionService.GetFilteredTransactions("Expense", null, null, null);
            return View(transactions);
        }

        // Income view
        public IActionResult Income()
        {
            var transactions = _transactionService.GetFilteredTransactions("Income", null, null, null);
            return View(transactions);
        }

        // Assets & Liabilities view
        public IActionResult AssetsLiabilities()
        {
            ViewBag.TotalAssets = _transactionService.GetTotalAssets();
            ViewBag.TotalLiabilities = _transactionService.GetTotalLiabilities();

            var transactions = _transactionService.GetFilteredTransactions(null, null, null, null);
            return View(transactions);
        }
    }
}