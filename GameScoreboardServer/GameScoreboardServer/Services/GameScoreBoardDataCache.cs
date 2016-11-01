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
			
		public int AddScoreRecordToStorage(ScoreRecord record)
        {
			List<ScoreRecord> gameScoreRecords;
			m_gameScoreBoardCache.TryGetValue (record.GameName.ToLower (), out gameScoreRecords); 
			if (gameScoreRecords == null) 
			{
				gameScoreRecords = new List<ScoreRecord> (); 	
			}
			gameScoreRecords.Add(record); 
      
			m_gameScoreBoardCache.AddOrUpdate(record.GameName, gameScoreRecords, (id, oldScoreList) => gameScoreRecords);
			return m_gameScoreBoardCache.Count(); 

        }

        public IEnumerable<ScoreRecord> GetAllScoresForGame(string gameName)
        {
            List<ScoreRecord> recordFromCache;
            
            return m_gameScoreBoardCache.TryGetValue(gameName.ToLower(), out recordFromCache) ? recordFromCache : Enumerable.Empty<ScoreRecord>();
        }

		public IEnumerable<ScoreRecord> GetTopTenScoresForGame(string gameName)
		{
			List<ScoreRecord> recordFromCache;

			m_gameScoreBoardCache.TryGetValue (gameName.ToLower (), out recordFromCache); 

			if (recordFromCache != null) 
			{
				return recordFromCache.OrderByDescending (x => x.Score).Take(10); 	
			}
			return Enumerable.Empty<ScoreRecord>(); 
		}

		public IEnumerable<ScoreRecord> GetScoresForGame (string gameName, int numberOfScores)
		{
			if (numberOfScores > 50) 
			{
				throw new ArgumentOutOfRangeException (); 
			}

			List<ScoreRecord> recordFromCache;

			m_gameScoreBoardCache.TryGetValue (gameName.ToLower (), out recordFromCache); 

			if (recordFromCache != null) 
			{
				return recordFromCache.OrderByDescending (x => x.Score).Take(numberOfScores); 	
			}
			return Enumerable.Empty<ScoreRecord>(); 
		}

		public IEnumerable<string> GetAllGameNames()
		{
			List<string> gameNames = new List<string>();
			foreach (var record in m_gameScoreBoardCache) 
			{
				gameNames.Add(record.Key); 
			}
			return gameNames; 
		}

        public IEnumerable<ScoreRecord> GetAllScoresForUsername(string username)
        {
            throw new NotImplementedException(); 
        }

		public int CountHigherScores (string gameName, int score)
		{
			throw new NotImplementedException ();
		}

		public void ClearCache()
		{
			m_gameScoreBoardCache.Clear(); 
		}


	}
}
