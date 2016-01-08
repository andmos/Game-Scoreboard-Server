using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GameScoreboardServer.Models;

namespace GameScoreboardServer.Services
{
    public class GameScoreBoardDataCache : IDataStorage
    {
        private readonly ConcurrentDictionary<string, List<GameScoreRecord>> m_gameScoreBoardCache;

        public bool AddScoreRecordToStorage(string gameName, GameScoreRecord record)
        {
            List<GameScoreRecord> gameScoreRecords = new List<GameScoreRecord>();
            if(m_gameScoreBoardCache.TryGetValue(gameName.ToLower(), out gameScoreRecords))
            {
                gameScoreRecords.Add(record); 
            }
            m_gameScoreBoardCache.AddOrUpdate(gameName, gameScoreRecords, (id, oldScoreList) => gameScoreRecords);
            return true; 

        }

        public IEnumerable<GameScoreRecord> GetAllScoresForGame(string gameName)
        {
            List<GameScoreRecord> recordFromCache;
            
            return m_gameScoreBoardCache.TryGetValue(gameName.ToLower(), out recordFromCache) ? recordFromCache : Enumerable.Empty<GameScoreRecord>();
        }

        public IEnumerable<GameScoreRecord> GetAllScoresForUsername(string username)
        {
            throw new NotImplementedException(); 
        }
    }
}
