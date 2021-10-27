namespace GameScoreboardServer.Web.Models
{
    public class ScoreRecord
    {
        public string GameName { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public int recordId { get; set; }
    }
}
