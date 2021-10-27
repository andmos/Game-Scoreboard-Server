using GameScoreboardServer.Web.Models;
using System.Collections.Generic;

namespace GameScoreboardServer.Web.Services
{
    public interface IScoreBoardRepository
    {
        IEnumerable<ScoreRecord> GetAllScoresForGame(string gameName);
        IEnumerable<ScoreRecord> GetTopTenScoresForGame(string gameName);
        IEnumerable<ScoreRecord> GetScoresForGame(string gameName, int numberOfScores);
        IEnumerable<ScoreRecord> GetAllScoresForUsername(string username);
        IEnumerable<string> GetAllGameNames();
        ScoreRecord GetScoreRecordForUsername(int recordId, string playerName);
        int CountHigherScores(string gameName, int score);
        int AddScoreRecordToStorage(ScoreRecord record);
    }
}
