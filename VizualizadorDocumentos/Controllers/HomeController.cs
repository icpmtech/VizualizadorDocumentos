using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VizualizadorDocumentos.Models;

namespace VizualizadorDocumentos.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var documentos = new List<DocumentViewModel>
        {
            new DocumentViewModel { Id = 1, Titulo = "Documento Simulado 1", Tipo = "PDF", Data = "2025-05-16" },
            new DocumentViewModel { Id = 2, Titulo = "Documento Simulado 2", Tipo = "PDF", Data = "2025-05-10" }
        };

            return View(documentos);
        }
        // Gera um PDF diretamente da memória
        public IActionResult Visualizar(int id)
        {
            var titulo = $"Documento Simulado #{id}";
            var content = $"Este é um conteúdo simulado para o documento com ID {id}. Gerado em {DateTime.Now:yyyy-MM-dd HH:mm:ss}.";

            // Simula um PDF como texto plano (podes integrar biblioteca de PDF real ex: iTextSharp, QuestPDF)
            var bytes = System.Text.Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(bytes);

            // Content-Type genérico, substitui por application/pdf se usares PDF real
            return File(stream, "application/octet-stream", $"{titulo}.txt");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
