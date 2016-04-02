using GameScoreboardServer.Models;
using GameScoreboardServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;


namespace TestGameScoreBoardServer.Tests.Services
{
	[TestFixture]
	public class TestGameScoreBoardDataCache 
	{     
		private GameScoreBoardDataCache m_gameScoreBoardCache; 

		public TestGameScoreBoardDataCache()
		{
			m_gameScoreBoardCache = new GameScoreBoardDataCache (); 
		}

		[Test]
		[Category("unit")]
		public void AddScoreRecordToStorage_GivenGameNameAndSingleScoreRecord_RecordGetsAddedToCache()
		{
			var record = new ScoreRecord { PlayerName = "Player1", Score = 3000, GameName = "game1"};
			string gameName = "Game1";

			m_gameScoreBoardCache.AddScoreRecordToStorage(record);

			Assert.AreEqual(m_gameScoreBoardCache.GetAllScoresForGame(gameName).FirstOrDefault().PlayerName, record.PlayerName);
		}

		[Test]
		[Category("unit")]
		public void GetAllScoresForGame_GivenValidGameName_ReturnsAllScoresForGame()
		{
			AddMultipleRecordsToCache(); 

			var recordsFromCache = m_gameScoreBoardCache.GetAllScoresForGame("game1");

			Assert.IsTrue (recordsFromCache.Count () == 15); 
		}

		[Test]
		[Category("unit")]
		public void GetTopTenScoresForGame_GivenValdigGameName_ReturnsTopTenRecordsSorted()
		{
			AddMultipleRecordsToCache();

			var recordsFromCache = m_gameScoreBoardCache.GetTopTenScoresForGame("game1");
			var sorted = recordsFromCache.OrderByDescending (x => x.Score); 

			Assert.IsTrue(recordsFromCache.Count () == 10); 
			CollectionAssert.AreEqual (sorted.ToList(), recordsFromCache.ToList()); 
		}
			
		[Test]
		[Category("unit")]
		public void GetScoresForGame_GivenValdigGameNameAndANumberOfScores_ReturnsCorrectNumberOfRecordsSorted()
		{
			AddMultipleRecordsToCache();

			var recordsFromCache = m_gameScoreBoardCache.GetScoresForGame("game1", 5);
			var sorted = recordsFromCache.OrderByDescending (x => x.Score); 

			Assert.IsTrue(recordsFromCache.Count () == 5); 
			CollectionAssert.AreEqual (sorted.ToList(), recordsFromCache.ToList()); 
		}

		[Test]
		[Category("unit")]
		public void GetScoresForGame_GivenGameNameNotInCache_ReturnsEmptyObject()
		{
			var recordsFromCache = m_gameScoreBoardCache.GetScoresForGame ("notValidName", 5); 

			Assert.IsTrue (recordsFromCache.SingleOrDefault() == null); 
		}

		[Test]
		[Category("unit")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void GetScoresForGame_GivenValdigGameNameAndANumberOfScoresOver50_ThrowsArgumentOutOfRangeException()
		{
			m_gameScoreBoardCache.GetScoresForGame("game1", 55);
		}

		[TearDown]
		public void TestTearDown()
		{
			m_gameScoreBoardCache.ClearCache (); 
		}

		private void AddMultipleRecordsToCache()
		{
			var records = new List<ScoreRecord> () 
			{
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 5000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 4000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 6000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 3000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 8000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 7000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 9000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 8000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 4000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 7000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 8000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 8000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 9000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player2", Score = 11000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player3", Score = 1000 }, 
			};
			foreach (var record in records) 
			{
				m_gameScoreBoardCache.AddScoreRecordToStorage(record); 
			}
		}
	
	}
}
