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
	[TestFixture()]
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

		[Test()]
		[Category("integration")]
		public void AddScoreRecordToStorage_GivenValidScoreRecord_ReturnsTrue()
		{
			var exampleRecord = new ScoreRecord { GameName = m_gameName, PlayerName = m_playerName, Score = 1000 };

			Assert.IsTrue (m_mysqlConnection.AddScoreRecordToStorage (exampleRecord));
		}

		[Test()]
		[Category("integration")]
		public void GetAllScoresForUsername_GivenValidUsername_ReturnsScoreForUsername()
		{
			var resultFromDb = m_mysqlConnection.GetAllScoresForUsername (m_playerName); 

			Assert.IsTrue (resultFromDb.Count() > 0); 
		}

		[Test()]
		[Category("integration")]
		public void GetAllScoresForGame_GivenValidGame_ReturnsScoreForGame()
		{
			var resultFromDb = m_mysqlConnection.GetAllScoresForGame (m_gameName);

			Assert.IsTrue (resultFromDb.Count() > 0); 
		}

		[Test()]
		[Category("integration")]
		public void GetTopTenScoresForGame_GivenListOfScores_ReturnsSortedListWIthTopTenScores()
		{
			AddMultipleRecordsToDatabase (); 

			var resultFromDb = m_mysqlConnection.GetTopTenScoresForGame (m_gameName);
			var sortedResult = resultFromDb.OrderBy (x => x.Score); 

			Assert.IsTrue(resultFromDb.Count() > 9); 
			CollectionAssert.AreEqual(sortedResult.ToList(), resultFromDb.ToList()); 
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
			};
			foreach (var record in records) 
			{
				m_mysqlConnection.AddScoreRecordToStorage (record); 
			}
		}

	}
}

