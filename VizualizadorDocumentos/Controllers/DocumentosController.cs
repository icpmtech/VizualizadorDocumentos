using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using VizualizadorDocumentos.Models;

namespace VizualizadorDocumentos.Controllers
{

    public class DocumentosController : Controller
    {
        private readonly ILogger<DocumentosController> _logger;

        public DocumentosController(ILogger<DocumentosController> logger)
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
        // Gera um PDF diretamente da mem�ria
        public IActionResult Visualizar(int id)
        {
            // Simula diferentes tipos de ficheiros com base no ID
            string tipo;
            string filename;
            byte[] bytes;

            switch (id)
            {
                case 1:
                    tipo = "application/pdf";
                    filename = "documento.pdf";
                    bytes = GerarSimulacaoPDF("Conte�do PDF simulado");
                    break;
                case 2:
                    tipo = "image/jpeg";
                    filename = "imagem.jpg";
                    bytes = GerarImagemSimulada(); // fun��o abaixo
                    break;
                case 3:
                    tipo = "image/tiff";
                    filename = "imagem.tif";
                    bytes = GerarImagemSimulada(); // mesmo conte�do para exemplo
                    break;
                default:
                    tipo = "text/plain";
                    filename = "documento.txt";
                    bytes = Encoding.UTF8.GetBytes($"Documento simulado ID {id} gerado em {DateTime.Now}");
                    break;
            }

            var stream = new MemoryStream(bytes);
            return File(stream, tipo, filename);
        }
        private byte[] GerarSimulacaoPDF(string texto)
        {
            return Encoding.UTF8.GetBytes(texto); // Substituir por lib PDF real (ex: QuestPDF)
        }
        private byte[] GerarImagemSimulada()
        {
            return null;
        }



    }
}
