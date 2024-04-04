using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMiniGigs.Models;

namespace ApiMiniGigs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryPaymentsController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        public HistoryPaymentsController(MiniGigsDBContext context)
        {
            _context = context;
        }

        // GET: api/HistoryPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryPayment>>> GetHistoryPayments()
        {
          if (_context.HistoryPayments == null)
          {
              return NotFound();
          }
            return await _context.HistoryPayments.ToListAsync();
        }

        // GET: api/HistoryPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoryPayment>> GetHistoryPayment(int id)
        {
          if (_context.HistoryPayments == null)
          {
              return NotFound();
          }
            var historyPayment = await _context.HistoryPayments.FindAsync(id);

            if (historyPayment == null)
            {
                return NotFound();
            }

            return historyPayment;
        }

        // GET: api/HistoryPayments/ByUserId/5
        [HttpGet("ByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<HistoryPayment>>> GetHistoryPaymentsByUserId(int userId)
        {
            // Используем LINQ запрос для получения всех записей HistoryPayment с указанным IdUser
            var historyPayments = await _context.HistoryPayments
                                            .Where(h => h.IdUser == userId)
                                            .ToListAsync();

            // Если записи не найдены, возвращаем NotFound
            if (historyPayments == null || !historyPayments.Any())
            {
                return NotFound();
            }

            // Возвращаем найденные записи
            return historyPayments;
        }


        // PUT: api/HistoryPayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryPayment(int id, HistoryPayment historyPayment)
        {
            if (id != historyPayment.IdHistoryPayment)
            {
                return BadRequest();
            }

            _context.Entry(historyPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryPaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HistoryPayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoryPayment>> PostHistoryPayment(HistoryPayment historyPayment)
        {
          if (_context.HistoryPayments == null)
          {
              return Problem("Entity set 'MiniGigsDBContext.HistoryPayments'  is null.");
          }
            _context.HistoryPayments.Add(historyPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistoryPayment", new { id = historyPayment.IdHistoryPayment }, historyPayment);
        }

        // DELETE: api/HistoryPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryPayment(int id)
        {
            if (_context.HistoryPayments == null)
            {
                return NotFound();
            }
            var historyPayment = await _context.HistoryPayments.FindAsync(id);
            if (historyPayment == null)
            {
                return NotFound();
            }

            _context.HistoryPayments.Remove(historyPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoryPaymentExists(int id)
        {
            return (_context.HistoryPayments?.Any(e => e.IdHistoryPayment == id)).GetValueOrDefault();
        }
    }
}
