using System;
using System.Linq; 
using System.Collections.Generic;
using GameScoreboardServer.Models;
using System.Diagnostics;


namespace GameScoreboardServer.Services
{
	public class DataStorageProfiler : IDataStorage
	{

		private readonly IDataStorage m_dataStorage;
		private readonly ILog m_logger;

		public DataStorageProfiler (IDataStorage dataStorage, ILog logger)
		{
			m_dataStorage = dataStorage; 
			m_logger = logger;
		}

		public IEnumerable<ScoreRecord> GetAllScoresForGame (string gameName)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = m_dataStorage.GetAllScoresForGame (gameName);
			stopWatch.Stop (); 
			m_logger.Info (string.Format ("{0} GetAllScoresForGame: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
			return scores; 
		}

		public IEnumerable<ScoreRecord> GetTopTenScoresForGame (string gameName)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = m_dataStorage.GetTopTenScoresForGame (gameName);
			stopWatch.Stop ();
			m_logger.Info(string.Format ("{0} GetTopTenScoresForGame: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
			return scores;
		}

		public IEnumerable<ScoreRecord> GetScoresForGame (string gameName, int numberOfScores)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = m_dataStorage.GetScoresForGame (gameName, numberOfScores);
			stopWatch.Stop ();
			m_logger.Info(string.Format ("{0} GetScoresForGame: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
			return scores;
		}

		public IEnumerable<ScoreRecord> GetAllScoresForUsername (string username)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = m_dataStorage.GetAllScoresForUsername (username);
			stopWatch.Stop ();
			m_logger.Info(string.Format ("{0} GetAllScoresForUsername: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
			return scores;
		}

		public int CountHigherScores (string gameName, int score)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = m_dataStorage.CountHigherScores(gameName, score);
			stopWatch.Stop ();
			m_logger.Info(string.Format ("{0} CountHigherScores: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
			return scores;
		}

		public IEnumerable<string> GetAllGameNames()
		{
			var stopWatch = Stopwatch.StartNew();
			var gameNames = m_dataStorage.GetAllGameNames();
			stopWatch.Stop();
			m_logger.Info(string.Format("{0} GetAllGameNames: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
			return gameNames;
		}

		public int AddScoreRecordToStorage (ScoreRecord record)
		{
			var stopWatch = Stopwatch.StartNew ();
			var id = m_dataStorage.AddScoreRecordToStorage(record);
			stopWatch.Stop ();
			m_logger.Info(string.Format ("{0} AddScoreRecordToStorage: Call took {1} Ms", m_dataStorage.GetType(), stopWatch.ElapsedMilliseconds));
			return(id);
		}


	}
}

