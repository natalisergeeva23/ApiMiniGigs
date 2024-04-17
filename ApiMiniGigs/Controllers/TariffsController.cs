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
    public class TariffsController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        public TariffsController(MiniGigsDBContext context)
        {
            _context = context;
        }

        // GET: api/Tariffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tariff>>> GetTariffs()
        {
          if (_context.Tariffs == null)
          {
              return NotFound();
          }
            return await _context.Tariffs.ToListAsync();
        }

        // GET: api/Tariffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tariff>> GetTariff(int id)
        {
          if (_context.Tariffs == null)
          {
              return NotFound();
          }
            var tariff = await _context.Tariffs.FindAsync(id);

            if (tariff == null)
            {
                return NotFound();
            }

            return tariff;
        }

        // PUT: api/Tariffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTariff(int id, Tariff tariff)
        {
            if (id != tariff.IdTariff)
            {
                return BadRequest();
            }

            _context.Entry(tariff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TariffExists(id))
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

        // POST: api/Tariffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tariff>> PostTariff(Tariff tariff)
        {
          if (_context.Tariffs == null)
          {
              return Problem("Entity set 'MiniGigsDBContext.Tariffs'  is null.");
          }
            _context.Tariffs.Add(tariff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTariff", new { id = tariff.IdTariff }, tariff);
        }

        // DELETE: api/Tariffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTariff(int id)
        {
            if (_context.Tariffs == null)
            {
                return NotFound();
            }
            var tariff = await _context.Tariffs.FindAsync(id);
            if (tariff == null)
            {
                return NotFound();
            }

            _context.Tariffs.Remove(tariff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TariffExists(int id)
        {
            return (_context.Tariffs?.Any(e => e.IdTariff == id)).GetValueOrDefault();
        }
    }
}
