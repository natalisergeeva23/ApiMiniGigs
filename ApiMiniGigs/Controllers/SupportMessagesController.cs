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
    public class SupportMessagesController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        public SupportMessagesController(MiniGigsDBContext context)
        {
            _context = context;
        }

        // GET: api/SupportMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupportMessage>>> GetSupportMessages()
        {
          if (_context.SupportMessages == null)
          {
              return NotFound();
          }
            return await _context.SupportMessages.ToListAsync();
        }

        // GET: api/SupportMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupportMessage>> GetSupportMessage(int id)
        {
          if (_context.SupportMessages == null)
          {
              return NotFound();
          }
            var supportMessage = await _context.SupportMessages.FindAsync(id);

            if (supportMessage == null)
            {
                return NotFound();
            }

            return supportMessage;
        }

        // PUT: api/SupportMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupportMessage(int id, SupportMessage supportMessage)
        {
            if (id != supportMessage.IdMessage)
            {
                return BadRequest();
            }

            _context.Entry(supportMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupportMessageExists(id))
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

        // POST: api/SupportMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SupportMessage>> PostSupportMessage(SupportMessage supportMessage)
        {
          if (_context.SupportMessages == null)
          {
              return Problem("Entity set 'MiniGigsDBContext.SupportMessages'  is null.");
          }
            _context.SupportMessages.Add(supportMessage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SupportMessageExists(supportMessage.IdMessage))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSupportMessage", new { id = supportMessage.IdMessage }, supportMessage);
        }

        // DELETE: api/SupportMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportMessage(int id)
        {
            if (_context.SupportMessages == null)
            {
                return NotFound();
            }
            var supportMessage = await _context.SupportMessages.FindAsync(id);
            if (supportMessage == null)
            {
                return NotFound();
            }

            _context.SupportMessages.Remove(supportMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupportMessageExists(int id)
        {
            return (_context.SupportMessages?.Any(e => e.IdMessage == id)).GetValueOrDefault();
        }
    }
}
