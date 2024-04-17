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
    public class MyWorksController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        public MyWorksController(MiniGigsDBContext context)
        {
            _context = context;
        }

        // GET: api/MyWorks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyWork>>> GetMyWorks()
        {
          if (_context.MyWorks == null)
          {
              return NotFound();
          }
            return await _context.MyWorks.ToListAsync();
        }
        // GET: api/MyWorks/ByUserId/5
        [HttpGet("ByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<MyWork>>> GetMyWorksByUserId(int userId)
        {
            var myWorks = await _context.MyWorks
                .Where(m => m.IdUser == userId)
                .Include(m => m.Order)
                .ThenInclude(o => o.User)
                .Select(m => new
                {
                    IdMyWork = m.IdMyWork,
                    IdOrder = m.IdOrder,
                    IdUser = m.IdUser,
                    NameWorkStatus = m.NameWorkStatus,
                    TaskDescription = m.Order.TaskDescription,
                    Link = m.Order.Link,
                    Order = new
                    {
                        Title = m.Order.Title,
                        Cost = m.Order.Cost,
                        User = new
                        {
                            Email = m.Order.User.Email
                        }
                    }
                })
                .ToListAsync();

            if (myWorks == null || myWorks.Count == 0)
            {
                return NotFound();
            }

            return Ok(myWorks);
        }


        // GET: api/MyWorks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyWork>> GetMyWork(int id)
        {
          if (_context.MyWorks == null)
          {
              return NotFound();
          }
            var myWork = await _context.MyWorks.FindAsync(id);

            if (myWork == null)
            {
                return NotFound();
            }

            return myWork;
        }

        // PUT: api/MyWorks/5/updatestatus
        // Обновление только NameWorkStatus для объекта MyWork
        [HttpPut("{id}/updatestatus")]
        public async Task<IActionResult> PutMyWorkNameWorkStatus(int id, [FromBody] string nameWorkStatus)
        {
            // Поиск существующей работы по id
            var myWork = await _context.MyWorks.FindAsync(id);
            if (myWork == null)
            {
                return NotFound();
            }

            // Обновление только NameWorkStatus
            myWork.NameWorkStatus = nameWorkStatus;

            // Отметить поле NameWorkStatus как измененное
            _context.Entry(myWork).Property(x => x.NameWorkStatus).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MyWorkExists(id))
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



        // PUT: api/MyWorks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMyWork(int id, MyWork myWork)
        {
            if (id != myWork.IdMyWork)
            {
                return BadRequest();
            }

            _context.Entry(myWork).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyWorkExists(id))
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

        // POST: api/MyWorks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MyWork>> PostMyWork(MyWork myWork)
        {
          if (_context.MyWorks == null)
          {
              return Problem("Entity set 'MiniGigsDBContext.MyWorks'  is null.");
          }
            _context.MyWorks.Add(myWork);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMyWork", new { id = myWork.IdMyWork }, myWork);
        }

        // DELETE: api/MyWorks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyWork(int id)
        {
            if (_context.MyWorks == null)
            {
                return NotFound();
            }
            var myWork = await _context.MyWorks.FindAsync(id);
            if (myWork == null)
            {
                return NotFound();
            }

            _context.MyWorks.Remove(myWork);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MyWorkExists(int id)
        {
            return (_context.MyWorks?.Any(e => e.IdMyWork == id)).GetValueOrDefault();
        }
    }
}
