using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GameScoreboardServer.Web.Models;

namespace GameScoreboardServer.Web.Services
{
	public class GameScoreBoardDataCache : IScoreBoardRepository
    {
        private readonly ConcurrentDictionary<string, List<ScoreRecord>> _gameScoreBoardCache;

		public GameScoreBoardDataCache()
		{
			_gameScoreBoardCache = new ConcurrentDictionary<string, List<ScoreRecord>> (); 
		}

		public int AddScoreRecordToStorage(ScoreRecord record)
        {
            _gameScoreBoardCache.TryGetValue(record.GameName.ToLower(), out List<ScoreRecord> gameScoreRecords);
            gameScoreRecords ??= new List<ScoreRecord>();

            record.recordId = _gameScoreBoardCache.Count + 1;
			gameScoreRecords.Add(record); 
      
			_gameScoreBoardCache.AddOrUpdate(record.GameName, gameScoreRecords, (id, oldScoreList) => gameScoreRecords);
			return record.recordId; 

        }

        public IEnumerable<ScoreRecord> GetAllScoresForGame(string gameName)
        {

            return _gameScoreBoardCache.TryGetValue(gameName.ToLower(), out List<ScoreRecord> recordFromCache) ? recordFromCache : Enumerable.Empty<ScoreRecord>();
        }

		public IEnumerable<ScoreRecord> GetTopTenScoresForGame(string gameName)
		{
            _gameScoreBoardCache.TryGetValue(gameName.ToLower(), out List<ScoreRecord> recordFromCache);

            return recordFromCache != null ? recordFromCache.OrderByDescending (x => x.Score).Take(10) : Enumerable.Empty<ScoreRecord>();
		}

		public IEnumerable<ScoreRecord> GetScoresForGame (string gameName, int numberOfScores)
		{
			if (numberOfScores > 50) 
			{
                throw new ArgumentOutOfRangeException();
            }

            _gameScoreBoardCache.TryGetValue(gameName.ToLower(), out List<ScoreRecord> recordFromCache);

            return recordFromCache != null ? recordFromCache.OrderByDescending (x => x.Score).Take(numberOfScores) : Enumerable.Empty<ScoreRecord>();
		}

		public IEnumerable<string> GetAllGameNames()
		{
			return _gameScoreBoardCache.Select(record => record.Key).ToList();
		}

		public ScoreRecord GetScoreRecordForUsername(int recordId, string playerName)
		{
			return (
				from records 
					in _gameScoreBoardCache.Values 
				from record in records 
				where record.PlayerName.ToLower().Equals(playerName.ToLower()) && record.recordId == recordId
				select record).FirstOrDefault();
		}

		public IEnumerable<ScoreRecord> GetAllScoresForUsername(string username)
		{
			return (
				from records 
					in _gameScoreBoardCache.Values 
				from record in records 
				where record.PlayerName.ToLower().Equals(username.ToLower()) 
				select record).ToList();
		}

		public int CountHigherScores (string gameName, int score)
		{
			throw new NotImplementedException ();
		}

		public void ClearCache()
		{
			_gameScoreBoardCache.Clear(); 
		}


	}
}
