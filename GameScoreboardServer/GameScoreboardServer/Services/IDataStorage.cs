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
        IEnumerable<GameScoreRecord> GetAllScoresForGame(string gameName);
        IEnumerable<GameScoreRecord> GetAllScoresForUsername(string username);
        bool AddScoreRecordToStorage(string gameName, GameScoreRecord record);
    }
}
