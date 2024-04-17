using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiMiniGigs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace ApiMiniGigs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImportController(MiniGigsDBContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("users/excel")]
        public async Task<IActionResult> ImportUsersFromExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("File is empty.");
            }

            // Читаем файл Excel
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        return BadRequest("Worksheet not found in the Excel file.");
                    }

                    // Проходим по строкам и добавляем пользователей
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var user = new User
                        {
                            Email = worksheet.Cells[row, 2].Value?.ToString(),
                            PhoneNumber = worksheet.Cells[row, 3].Value?.ToString(),
                            Name = worksheet.Cells[row, 4].Value?.ToString(),
                            Nickname = worksheet.Cells[row, 5].Value?.ToString(),
                            Amount = Convert.ToDecimal(worksheet.Cells[row, 6].Value),
                            BirthDate = Convert.ToDateTime(worksheet.Cells[row, 7].Value),
                            Gender = worksheet.Cells[row, 8].Value?.ToString()
                        };

                        // Добавляем пользователя в контекст
                        _context.Users.Add(user);
                    }

                    // Сохраняем изменения
                    await _context.SaveChangesAsync();
                }
            }

            return Ok("Users imported successfully.");
        }
    }
}
