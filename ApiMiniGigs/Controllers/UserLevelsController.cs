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
    public class UserLevelsController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        public UserLevelsController(MiniGigsDBContext context)
        {
            _context = context;
        }

        // GET: api/UserLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLevel>>> GetUserLevels()
        {
          if (_context.UserLevels == null)
          {
              return NotFound();
          }
            return await _context.UserLevels.ToListAsync();
        }

        // GET: api/UserLevels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLevel>> GetUserLevel(int id)
        {
          if (_context.UserLevels == null)
          {
              return NotFound();
          }
            var userLevel = await _context.UserLevels.FindAsync(id);

            if (userLevel == null)
            {
                return NotFound();
            }

            return userLevel;
        }

        // PUT: api/UserLevels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLevel(int id, UserLevel userLevel)
        {
            if (id != userLevel.IdUserLevel)
            {
                return BadRequest();
            }

            _context.Entry(userLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLevelExists(id))
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

        // POST: api/UserLevels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserLevel>> PostUserLevel(UserLevel userLevel)
        {
          if (_context.UserLevels == null)
          {
              return Problem("Entity set 'MiniGigsDBContext.UserLevels'  is null.");
          }
            _context.UserLevels.Add(userLevel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserLevelExists(userLevel.IdUserLevel))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserLevel", new { id = userLevel.IdUserLevel }, userLevel);
        }

        // DELETE: api/UserLevels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLevel(int id)
        {
            if (_context.UserLevels == null)
            {
                return NotFound();
            }
            var userLevel = await _context.UserLevels.FindAsync(id);
            if (userLevel == null)
            {
                return NotFound();
            }

            _context.UserLevels.Remove(userLevel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserLevelExists(int id)
        {
            return (_context.UserLevels?.Any(e => e.IdUserLevel == id)).GetValueOrDefault();
        }
    }
}
