using ProjQuakeLogParser.APPLICATION.Models;

namespace ProjQuakeLogParser.APPLICATION.Intefaces
{
    public interface IQuakeLogParser
    {

        Task<ResultCustomData<QuakeParseResponse>> GerarRelatorioLogs(string caminhoArquivo);
    }
}
