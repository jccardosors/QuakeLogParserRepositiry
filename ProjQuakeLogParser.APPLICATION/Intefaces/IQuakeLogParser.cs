using ProjQuakeLogParser.APPLICATION.Models;

namespace ProjQuakeLogParser.APPLICATION.Intefaces
{
    public interface IQuakeLogParser
    {

        Task<QuakeParseResponse> GerarRelatorioLogs(string caminhoArquivo);
    }
}
