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
    public class BalancesController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        public BalancesController(MiniGigsDBContext context)
        {
            _context = context;
        }

        // GET: api/Balances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Balance>>> GetBalances()
        {
          if (_context.Balances == null)
          {
              return NotFound();
          }
            return await _context.Balances.ToListAsync();
        }

        // GET: api/Balances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Balance>> GetBalance(int id)
        {
          if (_context.Balances == null)
          {
              return NotFound();
          }
            var balance = await _context.Balances.FindAsync(id);

            if (balance == null)
            {
                return NotFound();
            }

            return balance;
        }

        // PUT: api/Balances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBalance(int id, Balance balance)
        {
            if (id != balance.IdBalance)
            {
                return BadRequest();
            }

            _context.Entry(balance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BalanceExists(id))
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

        // POST: api/Balances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Balance>> PostBalance(Balance balance)
        {
          if (_context.Balances == null)
          {
              return Problem("Entity set 'MiniGigsDBContext.Balances'  is null.");
          }
            _context.Balances.Add(balance);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BalanceExists(balance.IdBalance))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBalance", new { id = balance.IdBalance }, balance);
        }

        // DELETE: api/Balances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBalance(int id)
        {
            if (_context.Balances == null)
            {
                return NotFound();
            }
            var balance = await _context.Balances.FindAsync(id);
            if (balance == null)
            {
                return NotFound();
            }

            _context.Balances.Remove(balance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BalanceExists(int id)
        {
            return (_context.Balances?.Any(e => e.IdBalance == id)).GetValueOrDefault();
        }
    }
}
