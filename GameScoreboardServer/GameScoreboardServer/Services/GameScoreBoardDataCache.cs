using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GameScoreboardServer.Models;

namespace GameScoreboardServer.Services
{
    public class GameScoreBoardDataCache : IDataStorage
    {
        private readonly ConcurrentDictionary<string, List<ScoreRecord>> m_gameScoreBoardCache;

		public GameScoreBoardDataCache()
		{
			m_gameScoreBoardCache = new ConcurrentDictionary<string, List<ScoreRecord>> (); 
		}

		public bool AddScoreRecordToStorage(ScoreRecord record)
        {
			List<ScoreRecord> gameScoreRecords;
			m_gameScoreBoardCache.TryGetValue (record.GameName.ToLower (), out gameScoreRecords); 
			if (gameScoreRecords == null) 
			{
				gameScoreRecords = new List<ScoreRecord> (); 	
			}
			gameScoreRecords.Add(record); 
      
			m_gameScoreBoardCache.AddOrUpdate(record.GameName, gameScoreRecords, (id, oldScoreList) => gameScoreRecords);
            return true; 

        }

        public IEnumerable<ScoreRecord> GetAllScoresForGame(string gameName)
        {
            List<ScoreRecord> recordFromCache;
            
            return m_gameScoreBoardCache.TryGetValue(gameName.ToLower(), out recordFromCache) ? recordFromCache : Enumerable.Empty<ScoreRecord>();
        }

		public IEnumerable<ScoreRecord> GetTopTenScoresForGame(string gameName)
		{
			return new List<ScoreRecord> ();  
		}

        public IEnumerable<ScoreRecord> GetAllScoresForUsername(string username)
        {
            throw new NotImplementedException(); 
        }
    }
}
