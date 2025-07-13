using ProjQuakeLogParser.APPLICATION.Intefaces;
using ProjQuakeLogParser.APPLICATION.Models;

namespace ProjQuakeLogParser.APPLICATION.Services
{
    public class QuakeLogParserService : IQuakeLogParser
    {
        public async Task<ResultCustomData<QuakeParseResponse>> GerarRelatorioLogs(string caminhoArquivo)
        {
            var dadosParser = new QuakeParseResponse() { data = new Dictionary<string, Jogo>() };
            var jogo = new Jogo();
            var jogadores = new List<string>();
            var mortos = new Dictionary<string, int>();
            var mortosPeloWorld = new Dictionary<string, int>();
            var nomeJogador = string.Empty;
            var contadorJogo = 0;

            try
            {
                using StreamReader reader = new(caminhoArquivo);

                while (reader.EndOfStream != true)
                {
                    var linha = reader.ReadLine();
                    if (!string.IsNullOrEmpty(linha))
                    {
                        //verifica inicio do game
                        if (linha.Contains("InitGame"))
                        {
                            ProcessaInicioJogo(ref jogo, ref contadorJogo);
                        }

                        //identifica o player
                        if (linha.Contains("ClientUserinfoChanged"))
                        {
                            ProcessaIndenficacaoJogador(ref nomeJogador, linha, ref jogadores);
                        }

                        //verifica se é uma morte
                        if (linha.Contains("Kill"))
                        {
                            ProcessaMorteJogador(linha, mortos, mortosPeloWorld);
                        }

                        //verifica se o game terminou
                        if (linha.Contains("ShutdownGame"))
                        {
                            dadosParser.data.Add($"game_{contadorJogo}", ProcessarTotalJogo(jogo, jogadores, mortos, mortosPeloWorld));

                            LimparDadosJogo(nomeJogador, jogo, jogadores, mortos, mortosPeloWorld);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultCustomData<QuakeParseResponse>.Falha("Erro ao processar o arquivo de log: " + ex.Message);
            }

            return await Task.FromResult(ResultCustomData<QuakeParseResponse>.Sucesso(dadosParser));
        }

        #region Private Methods

        private void ProcessaInicioJogo(ref Jogo jogo, ref int contadorJogo)
        {
            jogo = new Jogo() { Total_kills = 0, Players = new string[] { }, kills = new Dictionary<string, int>() };
            contadorJogo++;
        }

        private void ProcessaIndenficacaoJogador(ref string nomeJogador, string linha, ref List<string> jogadores)
        {
            string[] delimitadorStrings = [@"n\", @"\t"];
            nomeJogador = linha.Split(delimitadorStrings, StringSplitOptions.RemoveEmptyEntries)[1];

            if (!jogadores.Contains(nomeJogador))
            {
                jogadores.Add(nomeJogador.Trim());
            }
        }

        private void ProcessaMorteJogador(string linha, Dictionary<string, int> mortos, Dictionary<string, int> mortosPeloWorld)
        {
            string[] delimitadorStrings = [@"killed", @"by"];
            var nomeMorto = linha.Split(delimitadorStrings, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

            if (mortos.ContainsKey(nomeMorto))
            {
                mortos[nomeMorto]++;
            }
            else
            {
                mortos.Add(nomeMorto, 1);
            }

            if (linha.Contains("<world>"))
            {
                if (!mortosPeloWorld.ContainsKey(nomeMorto))
                {
                    mortosPeloWorld.Add(nomeMorto, 0);
                    mortosPeloWorld[nomeMorto]++;
                }
            }
        }

        private Jogo ProcessarTotalJogo(Jogo jogo, List<string> jogadores, Dictionary<string, int> mortos, Dictionary<string, int> mortosPeloWorld)
        {
            jogo.Players = new string[jogadores.Count()];
            var jogadoresOrdered = jogadores.OrderBy(x => x).ToList();

            for (int i = 0; i < jogadoresOrdered.Count(); i++)
            {
                jogo.Players[i] = jogadoresOrdered[i];
            }

            var mortosOrded = mortos.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in mortosOrded)
            {
                jogo.kills.Add(item.Key, item.Value);

                var mortoPeloWorld = mortosPeloWorld.Where(x => x.Key == item.Key).FirstOrDefault();
                if (mortoPeloWorld.Key != null)
                {
                    jogo.kills[item.Key]--;
                    jogo.Total_kills++;
                }

                jogo.Total_kills += item.Value;
            }

            return jogo;
        }

        private void LimparDadosJogo(string nomeJogador, Jogo jogo, List<string> jogadores, Dictionary<string, int> mortos, Dictionary<string, int> mortosPeloWorld)
        {
            jogo = null;
            nomeJogador = string.Empty;
            jogadores.Clear();

            foreach (var item in mortos.Keys)
            {
                mortos.Remove(item);
            }

            foreach (var item in mortosPeloWorld.Keys)
            {
                mortosPeloWorld.Remove(item);
            }
        }

        #endregion
    }
}
