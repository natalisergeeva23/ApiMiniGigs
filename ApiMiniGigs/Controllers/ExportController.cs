using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiMiniGigs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace ApiMiniGigs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ExportController(MiniGigsDBContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("users/excel")]
        public async Task<IActionResult> ExportUsersToExcel()
        {
            // Получаем список пользователей из базы данных
            List<User> users = await _context.Users.ToListAsync();

            // Создаем папку exports, если она не существует
            var exportFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "exports");
            if (!Directory.Exists(exportFolderPath))
            {
                Directory.CreateDirectory(exportFolderPath);
            }

            // Создаем новый файл Excel
            var filePath = Path.Combine(exportFolderPath, $"Users_{DateTime.Now:yyyyMMddHHmmss}.xlsx");

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Добавляем лист Excel
                var worksheet = package.Workbook.Worksheets.Add("Users");

                // Добавляем заголовки столбцов
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Email";
                worksheet.Cells[1, 3].Value = "Phone Number";
                worksheet.Cells[1, 4].Value = "Name";
                worksheet.Cells[1, 5].Value = "Nickname";
                worksheet.Cells[1, 6].Value = "Amount";
                worksheet.Cells[1, 7].Value = "Birth Date";
                worksheet.Cells[1, 8].Value = "Gender";

                // Заполняем ячейки данными о пользователях
                for (int i = 0; i < users.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = users[i].IdUser;
                    worksheet.Cells[i + 2, 2].Value = users[i].Email;
                    worksheet.Cells[i + 2, 3].Value = users[i].PhoneNumber;
                    worksheet.Cells[i + 2, 4].Value = users[i].Name;
                    worksheet.Cells[i + 2, 5].Value = users[i].Nickname;
                    worksheet.Cells[i + 2, 6].Value = users[i].Amount;
                    worksheet.Cells[i + 2, 7].Value = users[i].BirthDate?.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 8].Value = users[i].Gender;
                }

                // Сохраняем файл Excel на сервере
                package.Save();
            }

            // Возвращаем файл Excel для скачивания
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(filePath));
        }
    }
}
