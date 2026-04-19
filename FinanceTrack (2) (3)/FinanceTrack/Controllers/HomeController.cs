using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinanceTrack.Models;
using FinanceTrack.Interfaces;

namespace FinanceTrack.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITransactionService _transactionService;

        public HomeController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public IActionResult Index()
        {
            ViewBag.TotalBalance = _transactionService.GetTotalBalance();
            ViewBag.NetProfit = _transactionService.GetNetProfit();
            ViewBag.TotalAssets = _transactionService.GetTotalAssets();
            ViewBag.TotalLiabilities = _transactionService.GetTotalLiabilities();
            
            // Calculate Net Position
            ViewBag.NetPosition = ViewBag.TotalAssets - ViewBag.TotalLiabilities;

            // Pass chart data (simplified for POC)
            ViewBag.RecentTransactions = _transactionService.GetRecentTransactions(5);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
