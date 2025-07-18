﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ProjQuakeLogParser.API.Controllers;
using ProjQuakeLogParser.APPLICATION.Intefaces;
using ProjQuakeLogParser.APPLICATION.Models;

namespace ProjQuakerLogParser.TEST.ControllerTests
{
    public class QuakeLogParserTests
    {
        [Fact]
        public async Task GerarRelatorioLogs_SUCESSO()
        {
            //assert    
            var iQuakeLogParseMock = new Mock<IQuakeLogParser>();
            var configurationMock = new Mock<IConfiguration>();

            iQuakeLogParseMock.Setup(p => p.GerarRelatorioLogs(It.IsAny<string>())).Returns(GerarRelatorioLogsRetornoMock());
            configurationMock.SetupGet(x => x[It.Is<string>(s => s == "ArquivoLogQuake")]).Returns("games.log");

            //act
            var controller = new QuakeLogParserController(iQuakeLogParseMock.Object, configurationMock.Object);
            var response = await controller.GerarRelatorioLogs();

            //assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<ResultCustomData<QuakeParseResponse>>(okObjectResult.Value);

            Assert.NotNull(response);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, okObjectResult.StatusCode);
            Assert.True(result.data.data.Values.Count() >= 0);
            Assert.True(result.data.data.Values.ToList()[0].Total_kills == 10);
            Assert.True(result.data.data.Values.ToList()[0].Players.Contains("Isgalamido"));
            Assert.True(result.data.data.Values.ToList()[0].kills.Count() >= 0);
        }

        [Fact]
        public async Task GerarRelatorioLogs_FALHA()
        {
            //assert    
            var iQuakeLogParseMock = new Mock<IQuakeLogParser>();
            var configurationMock = new Mock<IConfiguration>();

            iQuakeLogParseMock.Setup(p => p.GerarRelatorioLogs(It.IsAny<string>())).Returns(GerarRelatorioLogsRetornoMock());
            configurationMock.SetupGet(x => x[It.Is<string>(s => s == "ArquivoLogQuake")]).Returns("");

            //act
            var controller = new QuakeLogParserController(iQuakeLogParseMock.Object, configurationMock.Object);
            var response = await controller.GerarRelatorioLogs();

            //assert
            var BadRequestObjectResult = Assert.IsType<BadRequestObjectResult>(response);

            Assert.NotNull(response);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, BadRequestObjectResult.StatusCode);
            Assert.Equal("Caminho do arquivo de log não configurado.", BadRequestObjectResult.Value);
        }


        #region Mocks

        private async Task<ResultCustomData<QuakeParseResponse>> GerarRelatorioLogsRetornoMock()
        {
            var item = new Dictionary<string, Jogo>();

            item.Add("game_1", new Jogo
            {
                Total_kills = 10,
                Players = new string[] { "Dono da bola", "Isgalamido", "Zeh" },
                kills = new Dictionary<string, int>
                {
                    { "Dono da bola", 5 },
                    { "Isgalamido", 3 },
                    { "Zeh", 2 }
                }
            });

            var data = new QuakeParseResponse() { data = item };

            return await Task.FromResult(ResultCustomData<QuakeParseResponse>.Sucesso(data));
        }

        #endregion
    }
}
