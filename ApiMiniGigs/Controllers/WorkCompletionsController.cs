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
    public class WorkCompletionsController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        private readonly IWebHostEnvironment _environment;

        public WorkCompletionsController(MiniGigsDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // GET: api/WorkCompletions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkCompletion>>> GetWorkCompletions()
        {
          if (_context.WorkCompletions == null)
          {
              return NotFound();
          }
            return await _context.WorkCompletions.ToListAsync();
        }

        // GET: api/WorkCompletions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkCompletion>> GetWorkCompletion(int id)
        {
          if (_context.WorkCompletions == null)
          {
              return NotFound();
          }
            var workCompletion = await _context.WorkCompletions.FindAsync(id);

            if (workCompletion == null)
            {
                return NotFound();
            }

            return workCompletion;
        }

        // PUT: api/WorkCompletions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkCompletion(int id, WorkCompletion workCompletion)
        {
            if (id != workCompletion.IdWorkCompletion)
            {
                return BadRequest();
            }

            _context.Entry(workCompletion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkCompletionExists(id))
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

        [HttpPost]
        public async Task<IActionResult> FinishWork(IFormFile workPhoto, string comment, int orderId)
        {
            if (workPhoto != null && workPhoto.Length > 0)
            {
                // Создаём папку для загрузки, если она не существует
                var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                // Определение пути сохранения файла
                var filePath = Path.Combine(uploadsFolderPath, Guid.NewGuid().ToString() + Path.GetExtension(workPhoto.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await workPhoto.CopyToAsync(stream);
                }

                // Сохранение информации о завершении работы в базе данных
                var workCompletion = new WorkCompletion
                {
                    IdOrder = orderId,
                    Comment = comment,
                    PhotoPath = filePath
                };
                _context.WorkCompletions.Add(workCompletion);
                await _context.SaveChangesAsync();

                return Ok(); // Или другой подходящий ответ
            }

            return BadRequest("Проблема с загрузкой файла.");
        }


        // DELETE: api/WorkCompletions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkCompletion(int id)
        {
            if (_context.WorkCompletions == null)
            {
                return NotFound();
            }
            var workCompletion = await _context.WorkCompletions.FindAsync(id);
            if (workCompletion == null)
            {
                return NotFound();
            }

            _context.WorkCompletions.Remove(workCompletion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkCompletionExists(int id)
        {
            return (_context.WorkCompletions?.Any(e => e.IdWorkCompletion == id)).GetValueOrDefault();
        }
    }
}
