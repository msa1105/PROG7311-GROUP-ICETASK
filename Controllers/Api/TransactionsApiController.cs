using Microsoft.AspNetCore.Mvc;
using FinanceTrack.Interfaces;
using FinanceTrack.Models;

namespace FinanceTrack.Controllers.Api
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsApiController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsApiController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _transactionService.GetAllTransactions();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _transactionService.GetTransactionById(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Transaction transaction)
        {
            _transactionService.AddTransaction(transaction);
            return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transaction);
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            _transactionService.UpdateTransactionStatus(id, status);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Transaction transaction)
        {
            if (id != transaction.Id) return BadRequest();
            var existing = _transactionService.GetTransactionById(id);
            if (existing == null) return NotFound();
            _transactionService.UpdateTransaction(transaction);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _transactionService.GetTransactionById(id);
            if (existing == null) return NotFound();
            _transactionService.DeleteTransaction(id);
            return NoContent();
        }
    }
}
