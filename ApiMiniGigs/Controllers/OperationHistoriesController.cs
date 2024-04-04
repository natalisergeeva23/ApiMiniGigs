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
    public class OperationHistoriesController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        public OperationHistoriesController(MiniGigsDBContext context)
        {
            _context = context;
        }

        // GET: api/OperationHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationHistory>>> GetOperationHistories()
        {
          if (_context.OperationHistories == null)
          {
              return NotFound();
          }
            return await _context.OperationHistories.ToListAsync();
        }

        // GET: api/OperationHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationHistory>> GetOperationHistory(int id)
        {
          if (_context.OperationHistories == null)
          {
              return NotFound();
          }
            var operationHistory = await _context.OperationHistories.FindAsync(id);

            if (operationHistory == null)
            {
                return NotFound();
            }

            return operationHistory;
        }

        // PUT: api/OperationHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperationHistory(int id, OperationHistory operationHistory)
        {
            if (id != operationHistory.OperationNumber)
            {
                return BadRequest();
            }

            _context.Entry(operationHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationHistoryExists(id))
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

        // POST: api/OperationHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OperationHistory>> PostOperationHistory(OperationHistory operationHistory)
        {
          if (_context.OperationHistories == null)
          {
              return Problem("Entity set 'MiniGigsDBContext.OperationHistories'  is null.");
          }
            _context.OperationHistories.Add(operationHistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OperationHistoryExists(operationHistory.OperationNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOperationHistory", new { id = operationHistory.OperationNumber }, operationHistory);
        }

        // DELETE: api/OperationHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationHistory(int id)
        {
            if (_context.OperationHistories == null)
            {
                return NotFound();
            }
            var operationHistory = await _context.OperationHistories.FindAsync(id);
            if (operationHistory == null)
            {
                return NotFound();
            }

            _context.OperationHistories.Remove(operationHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperationHistoryExists(int id)
        {
            return (_context.OperationHistories?.Any(e => e.OperationNumber == id)).GetValueOrDefault();
        }
    }
}
