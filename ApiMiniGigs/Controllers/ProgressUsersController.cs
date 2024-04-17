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
    public class ProgressUsersController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        public ProgressUsersController(MiniGigsDBContext context)
        {
            _context = context;
        }

        // GET: api/ProgressUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgressUser>>> GetProgressUsers()
        {
          if (_context.ProgressUsers == null)
          {
              return NotFound();
          }
            return await _context.ProgressUsers.ToListAsync();
        }

        // GET: api/ProgressUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgressUser>> GetProgressUser(int id)
        {
          if (_context.ProgressUsers == null)
          {
              return NotFound();
          }
            var progressUser = await _context.ProgressUsers.FindAsync(id);

            if (progressUser == null)
            {
                return NotFound();
            }

            return progressUser;
        }

        // GET: api/ProgressUsers/ByUserId/5
        [HttpGet("ByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<ProgressUser>>> GetProgressUsersByUserId(int userId)
        {
            // Находим все записи о прогрессе пользователя с указанным IdUser
            var progressUsers = await _context.ProgressUsers.Where(p => p.IdUser == userId).ToListAsync();

            // Если записей не найдено, возвращаем пустой список
            if (progressUsers == null || !progressUsers.Any())
            {
                return Ok(new List<ProgressUser>());
            }

            // Возвращаем найденные записи о прогрессе пользователя
            return progressUsers;
        }


        // PUT: api/ProgressUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgressUser(int id, ProgressUser progressUser)
        {
            if (id != progressUser.IdProgressUser)
            {
                return BadRequest();
            }

            _context.Entry(progressUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressUserExists(id))
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


        // PUT: api/ProgressUsers/ByUserId/{userId}
        [HttpPut("ByUserIdPut/{userId}")]
        public async Task<IActionResult> PutProgressUserByUserId(int userId, ProgressUser progressUser)
        {
            if (userId != progressUser.IdUser)
            {
                return BadRequest();
            }

            var existingProgressUser = await _context.ProgressUsers.FirstOrDefaultAsync(p => p.IdUser == userId);

            if (existingProgressUser == null)
            {
                return NotFound();
            }

            existingProgressUser.Value = progressUser.Value;

            try
            {
                _context.ProgressUsers.Update(existingProgressUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressUserExists(userId))
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


        // POST: api/ProgressUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProgressUser>> PostProgressUser(ProgressUser progressUser)
        {
          if (_context.ProgressUsers == null)
          {
              return Problem("Entity set 'MiniGigsDBContext.ProgressUsers'  is null.");
          }
            _context.ProgressUsers.Add(progressUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgressUser", new { id = progressUser.IdProgressUser }, progressUser);
        }

        // DELETE: api/ProgressUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgressUser(int id)
        {
            if (_context.ProgressUsers == null)
            {
                return NotFound();
            }
            var progressUser = await _context.ProgressUsers.FindAsync(id);
            if (progressUser == null)
            {
                return NotFound();
            }

            _context.ProgressUsers.Remove(progressUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgressUserExists(int id)
        {
            return (_context.ProgressUsers?.Any(e => e.IdProgressUser == id)).GetValueOrDefault();
        }
    }
}
