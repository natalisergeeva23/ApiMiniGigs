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
    public class FinishedTasksController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        public FinishedTasksController(MiniGigsDBContext context)
        {
            _context = context;
        }

        // GET: api/FinishedTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinishedTask>>> GetFinishedTasks()
        {
          if (_context.FinishedTasks == null)
          {
              return NotFound();
          }
            return await _context.FinishedTasks.ToListAsync();
        }

        // GET: api/FinishedTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinishedTask>> GetFinishedTask(int id)
        {
          if (_context.FinishedTasks == null)
          {
              return NotFound();
          }
            var finishedTask = await _context.FinishedTasks.FindAsync(id);

            if (finishedTask == null)
            {
                return NotFound();
            }

            return finishedTask;
        }

        // PUT: api/FinishedTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinishedTask(int id, FinishedTask finishedTask)
        {
            if (id != finishedTask.IdFinishedTask)
            {
                return BadRequest();
            }

            _context.Entry(finishedTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinishedTaskExists(id))
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

        // POST: api/FinishedTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FinishedTask>> PostFinishedTask(FinishedTask finishedTask)
        {
          if (_context.FinishedTasks == null)
          {
              return Problem("Entity set 'MiniGigsDBContext.FinishedTasks'  is null.");
          }
            _context.FinishedTasks.Add(finishedTask);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FinishedTaskExists(finishedTask.IdFinishedTask))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFinishedTask", new { id = finishedTask.IdFinishedTask }, finishedTask);
        }

        // DELETE: api/FinishedTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinishedTask(int id)
        {
            if (_context.FinishedTasks == null)
            {
                return NotFound();
            }
            var finishedTask = await _context.FinishedTasks.FindAsync(id);
            if (finishedTask == null)
            {
                return NotFound();
            }

            _context.FinishedTasks.Remove(finishedTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinishedTaskExists(int id)
        {
            return (_context.FinishedTasks?.Any(e => e.IdFinishedTask == id)).GetValueOrDefault();
        }
    }
}
