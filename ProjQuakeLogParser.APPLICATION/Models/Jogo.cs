namespace ProjQuakeLogParser.APPLICATION.Models
{
    public class Jogo
    {
        public int Total_kills { get; set; }
        public string[] Players { get; set; } = new string[] { };
        public Dictionary<string, int> kills { get; set; } = new Dictionary<string, int>();
    }
}
