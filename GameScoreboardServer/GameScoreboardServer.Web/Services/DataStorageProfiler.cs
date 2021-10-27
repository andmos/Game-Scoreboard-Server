using System.Collections.Generic;
using System.Diagnostics;
using GameScoreboardServer.Web.Models;

namespace GameScoreboardServer.Web.Services
{
	public class DataStorageProfiler : IScoreBoardRepository
	{

		private readonly IScoreBoardRepository _dataStorage;
		private readonly ILog _logger;

		public DataStorageProfiler (IScoreBoardRepository dataStorage, ILog logger)
		{
			_dataStorage = dataStorage; 
			_logger = logger;
		}

		public IEnumerable<ScoreRecord> GetAllScoresForGame (string gameName)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = _dataStorage.GetAllScoresForGame (gameName);
			stopWatch.Stop (); 
			_logger.Info (
				$"{_dataStorage.GetType()} GetAllScoresForGame: Call took {stopWatch.ElapsedMilliseconds} Ms");
			return scores; 
		}

		public IEnumerable<ScoreRecord> GetTopTenScoresForGame (string gameName)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = _dataStorage.GetTopTenScoresForGame (gameName);
			stopWatch.Stop ();
			_logger.Info(
				$"{_dataStorage.GetType()} GetTopTenScoresForGame: Call took {stopWatch.ElapsedMilliseconds} Ms");
			return scores;
		}

		public IEnumerable<ScoreRecord> GetScoresForGame (string gameName, int numberOfScores)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = _dataStorage.GetScoresForGame (gameName, numberOfScores);
			stopWatch.Stop ();
			_logger.Info($"{_dataStorage.GetType()} GetScoresForGame: Call took {stopWatch.ElapsedMilliseconds} Ms");
			return scores;
		}

		public IEnumerable<ScoreRecord> GetAllScoresForUsername (string username)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = _dataStorage.GetAllScoresForUsername (username);
			stopWatch.Stop ();
			_logger.Info(
				$"{_dataStorage.GetType()} GetAllScoresForUsername: Call took {stopWatch.ElapsedMilliseconds} Ms");
			return scores;
		}

		public ScoreRecord GetScoreRecordForUsername(int recordId, string playerName)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = _dataStorage.GetScoreRecordForUsername(recordId, playerName);
			stopWatch.Stop ();
			_logger.Info(
				$"{_dataStorage.GetType()} GetScoreRecordForUsername: Call took {stopWatch.ElapsedMilliseconds} Ms");
			return scores;
		}

		public int CountHigherScores (string gameName, int score)
		{
			var stopWatch = Stopwatch.StartNew ();
			var scores = _dataStorage.CountHigherScores(gameName, score);
			stopWatch.Stop ();
			_logger.Info($"{_dataStorage.GetType()} CountHigherScores: Call took {stopWatch.ElapsedMilliseconds} Ms");
			return scores;
		}

		public IEnumerable<string> GetAllGameNames()
		{
			var stopWatch = Stopwatch.StartNew();
			var gameNames = _dataStorage.GetAllGameNames();
			stopWatch.Stop();
			_logger.Info($"{_dataStorage.GetType()} GetAllGameNames: Call took {stopWatch.ElapsedMilliseconds} Ms");
			return gameNames;
		}

		public int AddScoreRecordToStorage (ScoreRecord record)
		{
			var stopWatch = Stopwatch.StartNew ();
			var id = _dataStorage.AddScoreRecordToStorage(record);
			stopWatch.Stop ();
			_logger.Info(
				$"{_dataStorage.GetType()} AddScoreRecordToStorage: Call took {stopWatch.ElapsedMilliseconds} Ms");
			return(id);
		}


	}
}

