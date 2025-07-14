# Solution SlnQuakeLogParser
<p>
  <h2>[Descrição do Projeto]</h2>
 Projeto construído lêr um arquivo de log do jogo Quake 3 Arena. O log registra todas as informações dos jogos, quando um jogo começa, quando termina, quem matou quem, entre outros.
<br/>
O parser construído consegue ler o arquivo, agrupar os dados de cada jogo, e em cada jogo coleta informações de morte dos jogadores.
</p>
<p>
  <h2>[Tecnologias/Abordagens utilizadas]</h2>
  <ul>
    <li>.NET 9</li>
    <li>Swagger</li>
    <li>Web API Rest</li>
    <li>Padrão Result (Resposta API)</li>
    <li>Injeção de dependência</li>
    <li>Testes unitários</li>
    <li>xUnit</li>
  </ul>
</p>

<p>
  <h2>[Arquitetura]</h2>
  Utilizada uma abordagem de arquitetura clean separando cada projeto com suas responsabilidades:
  <ul>
    <li><b> Projeto ProjQuakeLogParser.API:</b> Projeto que representa a API, responsável por expor os endpoints, validações e armazenamento do arquivo de log.</li>
    <li><b> Projeto ProjQuakeLogParser.APPLICATION:</b> Projeto responsável por implementar as regras de negócio e tendo este como sua principal ação.</li>
    <li><b> Projeto ProjQuakeLogParser.IOC:</b> Projeto responsável por fazer o registro das Interfaces e suas classes de serviço (Injeção de dependência).</li>
    <li><b> Projeto ProjQuakeLogParser.TEST:</b> Projeto responsável por implementar os testes unitários das controllers e das classes de serviço.</li>
  </ul>
</p>

<p>
  <h2>[Acesso ao Projeto]</h2>
  Siga alguns passos abaixo para rodar o projeto:
  <ul>
    <li>Clone ou baixe o repositório pelo link <a href="#">https://github.com/jccardosors/QuakeLogParserRepositiry</a> </li>
    <li>Abra o projeto e defina o <b>ProjQuakeLogParser.API</b> como sendo o projeto <b>Start Project</b></li></li>
    <li>Com a api rodando consuma o endpoint <b>GerarRelatorioLogs</b> e este deve fazer a leitura do arquivo de log</li>
  </ul>
</p>

<p>
  <h2>[Autor]</h2>
  <img loading="lazy" src="https://avatars.githubusercontent.com/u/38966527?v=4" alt="Jose Claudio Cardoso" size="32" height="32" width="32">
  <b>Jose Claudio Cardoso</b>
</p>





  

























