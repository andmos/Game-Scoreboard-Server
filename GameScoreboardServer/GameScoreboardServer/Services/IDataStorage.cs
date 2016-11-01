using GameScoreboardServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameScoreboardServer.Services
{
    public interface IDataStorage
    {
        IEnumerable<ScoreRecord> GetAllScoresForGame(string gameName);
		IEnumerable<ScoreRecord> GetTopTenScoresForGame (string gameName); 
		IEnumerable<ScoreRecord> GetScoresForGame (string gameName, int numberOfScores); 
        IEnumerable<ScoreRecord> GetAllScoresForUsername(string username);
		IEnumerable<string> GetAllGameNames(); 
		int CountHigherScores (string gameName, int score);
        int AddScoreRecordToStorage(ScoreRecord record);
    }
}
