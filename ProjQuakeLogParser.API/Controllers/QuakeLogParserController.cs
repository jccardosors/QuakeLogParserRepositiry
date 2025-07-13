using Microsoft.AspNetCore.Mvc;
using ProjQuakeLogParser.APPLICATION.Intefaces;

namespace ProjQuakeLogParser.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuakeLogParserController : ControllerBase
    {
        private readonly IQuakeLogParser _quakeLogParser;
        private readonly IConfiguration _configuration;

        public QuakeLogParserController(IQuakeLogParser leitorArquivo, IConfiguration configuration)
        {
            _quakeLogParser = leitorArquivo;
            _configuration = configuration;
        }

        [HttpGet("GerarRelatorioLogs")]
        public async Task<IActionResult> GerarRelatorioLogs()
        {
            var caminhoArquivo = Environment.CurrentDirectory;
            var nomeArquivo = _configuration["ArquivoLogQuake"];

            caminhoArquivo = $"{caminhoArquivo}\\ResourceData\\{nomeArquivo}";
            if (string.IsNullOrEmpty(caminhoArquivo))
            {
                return BadRequest("Caminho do arquivo de log não configurado.");
            }

            var response = await _quakeLogParser.GerarRelatorioLogs(caminhoArquivo);

            return Ok(response);

        }
    }
}
