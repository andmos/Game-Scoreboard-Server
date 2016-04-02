using System;
using NUnit.Framework;
using System.Configuration;
using System.Linq;
using GameScoreboardServer;
using GameScoreboardServer.Models;
using MySql.Data.MySqlClient; 
using Dapper;
using System.Collections.Generic;

namespace TestGameScoreboardServer
{
	[TestFixture]
	public class TestGameScoreBoardMysqlConnection
	{

		private readonly string m_connectionString;
		private readonly GameScoreBoardMysqlConnection m_mysqlConnection; 
		private const string m_playerName = "player1"; 
		private const string m_gameName = "game1"; 

		public TestGameScoreBoardMysqlConnection()
		{
			m_connectionString = ConfigurationManager.AppSettings["ConnectionString"];
			m_mysqlConnection = new GameScoreBoardMysqlConnection (m_connectionString); 
		}

		[Test]
		[Category("integration")]
		public void AddScoreRecordToStorage_GivenValidScoreRecord_ReturnsIdFromDataStorage()
		{
			var exampleRecord = new ScoreRecord { GameName = m_gameName, PlayerName = m_playerName, Score = 1000 };

			Assert.IsTrue (m_mysqlConnection.AddScoreRecordToStorage (exampleRecord) > -1);
		}

		[Test]
		[Category("integration")]
		public void GetAllScoresForUsername_GivenValidUsername_ReturnsScoreForUsername()
		{
			var exampleRecord = new ScoreRecord { GameName = m_gameName, PlayerName = m_playerName, Score = 1000 };
			m_mysqlConnection.AddScoreRecordToStorage (exampleRecord); 

			var resultFromDb = m_mysqlConnection.GetAllScoresForUsername (m_playerName); 
			var playerRecord = resultFromDb.FirstOrDefault (); 

			Assert.IsTrue (resultFromDb.Count() > 0); 
			Assert.AreEqual (m_playerName, playerRecord.PlayerName);  
			Assert.AreEqual (1000, playerRecord.Score	); 
		}

		[Test]
		[Category("integration")]
		public void GetAllScoresForGame_GivenValidGame_ReturnsScoreForGame()
		{
			var resultFromDb = m_mysqlConnection.GetAllScoresForGame (m_gameName);

			Assert.IsTrue (resultFromDb.Count() > 0); 
			Assert.AreEqual (m_gameName, resultFromDb.FirstOrDefault().GameName);  
		}

		[Test]
		[Category("integration")]
		public void GetTopTenScoresForGame_GivenListOfScores_ReturnsSortedListWIthTopTenScores()
		{
			AddMultipleRecordsToDatabase (); 

			var resultFromDb = m_mysqlConnection.GetTopTenScoresForGame (m_gameName);
			var sortedResult = resultFromDb.OrderByDescending(x => x.Score); 

			Assert.IsTrue(resultFromDb.Count() > 9); 
			CollectionAssert.AreEqual(sortedResult.ToList(), resultFromDb.ToList()); 
		}

		[Test]
		[Category("integration")]
		public void GetScoresForGame_GivenValdigGameNameAndANumberOfScores_ReturnsCorrectNumberOfRecordsSorted()
		{
			AddMultipleRecordsToDatabase (); 

			var resultFromDb = m_mysqlConnection.GetScoresForGame(m_gameName, 5);
			var sortedResult = resultFromDb.OrderByDescending(x => x.Score); 

			Assert.IsTrue(resultFromDb.Count() == 5); 
			CollectionAssert.AreEqual(sortedResult.ToList(), resultFromDb.ToList()); 
		}

		[Test]
		[Category("integration")]
		public void CountHigherScores_GivenValidGameRecord_ReturnsCorrectCountOfHigherRecords()
		{
			AddMultipleRecordsToDatabase (); 

			var resultFromDb = m_mysqlConnection.CountHigherScores ("game5", 2000);

			Assert.IsTrue(resultFromDb >= 2);
		}

		[Test]
		[Category("integration")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void GetScoresForGame_GivenValdigGameNameAndANumberOfScoresOver50_ThrowsArgumentOutOfRangeException()
		{
			var resultFromDb = m_mysqlConnection.GetScoresForGame(m_gameName, 55);
		}

		private void AddMultipleRecordsToDatabase()
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
				new ScoreRecord { GameName = "game5", PlayerName = "player5", Score = 9000 }, 
				new ScoreRecord { GameName = "game5", PlayerName = "player5", Score = 11000 }, 
				new ScoreRecord { GameName = "game5", PlayerName = "player5", Score = 1000 }, 
			};
			foreach (var record in records) 
			{
				m_mysqlConnection.AddScoreRecordToStorage (record); 
			}
		}

	}
}

