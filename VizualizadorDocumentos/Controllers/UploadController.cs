using Microsoft.AspNetCore.Mvc;

namespace VizualizadorDocumentos.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.IO;
    using System.Threading.Tasks;

    public class UploadController : Controller
    {
        private readonly string _uploadPath;

        public UploadController(IWebHostEnvironment env)
        {
            _uploadPath = Path.Combine(env.WebRootPath, "uploads");
            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);
        }

        public IActionResult Index()
        {
            var files = Directory.GetFiles(_uploadPath)
                                 .Select(Path.GetFileName)
                                 .ToList();
            return View(files);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(_uploadPath, filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Preview(string file)
        {
            var filePath = Path.Combine(_uploadPath, file);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var mime = GetMimeType(file);
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream, mime);
        }

        private string GetMimeType(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            return ext switch
            {
                ".pdf" => "application/pdf",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".tif" or ".tiff" => "image/tiff",
                _ => "application/octet-stream",
            };
        }
    }

}
