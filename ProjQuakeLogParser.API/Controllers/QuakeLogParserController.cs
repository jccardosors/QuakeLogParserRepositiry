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

        /// <summary>
        /// Gera um relatório a partir do arquivo de log do Quake.
        /// </summary>
        /// <returns>Os logs do arquivo separados por game, jogadores e mortos</returns>
        /// <response code="200">Retorna o relatório gerado com sucesso.</response>
        [HttpGet("GerarRelatorioLogs")]
        public async Task<IActionResult> GerarRelatorioLogs()
        {
            string? caminhoArquivo = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string? nomeArquivo = _configuration["ArquivoLogQuake"];

            caminhoArquivo = $"{caminhoArquivo}\\ResourceData\\{nomeArquivo}";
            if (!System.IO.File.Exists(caminhoArquivo))
            {
                return BadRequest("Caminho do arquivo de log não configurado.");
            }

            var response = await _quakeLogParser.GerarRelatorioLogs(caminhoArquivo);

            return Ok(response);
        }
    }
}
