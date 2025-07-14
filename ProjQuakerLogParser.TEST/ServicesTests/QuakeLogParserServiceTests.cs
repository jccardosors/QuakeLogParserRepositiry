using ProjQuakeLogParser.APPLICATION.Services;

namespace ProjQuakerLogParser.TEST.ServicesTests
{
    public class QuakeLogParserServiceTests
    {
        [Fact]
        public async Task GerarRelatorioLogs_SUCESSO()
        {
            string? caminhoArquivo = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            caminhoArquivo = $"{caminhoArquivo}\\ResourceData\\games.log";

            //Act
            var quakeLogParserService = new QuakeLogParserService();
            var response = await quakeLogParserService.GerarRelatorioLogs(caminhoArquivo);

            //Assert
            Assert.NotNull(response.data);
            Assert.True(response.data.data.Any());
            Assert.True(response.data.data.Count() == 20);
            Assert.True(response.data.data.Values.ToList()[0].Total_kills == 0);
        }


        [Fact]
        public async Task GerarRelatorioLogs_FALHA()
        {
            string? caminhoArquivo = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            caminhoArquivo = $"{caminhoArquivo}\\ResourceData\\gameOver.log";

            //Act
            var quakeLogParserService = new QuakeLogParserService();
            var response = await quakeLogParserService.GerarRelatorioLogs(caminhoArquivo);

            //Assert
            Assert.Null(response.data);
            Assert.False(response.EhSucesso);
            Assert.Contains("Erro ao processar o arquivo de log:", response.Mensagem);
        }
    }
}
