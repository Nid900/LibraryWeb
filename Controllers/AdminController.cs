using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace LibraryWeb.Controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        [HttpGet("download-db")]
        public IActionResult DownloadDatabase()
        {
            // Проверяем основной путь в облаке Railway
            string dbPath = "/app/data/library.db";

            if (!System.IO.File.Exists(dbPath))
            {
                // Если там нет, ищем в папке приложения
                dbPath = Path.Combine(Directory.GetCurrentDirectory(), "library.db");
            }

            if (!System.IO.File.Exists(dbPath))
            {
                return NotFound("База данных не найдена. Попробуйте добавить хотя бы одну книгу на сайте, чтобы файл создался.");
            }

            // Читаем файл и отдаем на скачивание
            var fileBytes = System.IO.File.ReadAllBytes(dbPath);
            return File(fileBytes, "application/octet-stream", "library.db");
        }
    }
}
