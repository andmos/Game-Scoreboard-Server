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

		public TestGameScoreBoardMysqlConnection()
		{
			m_connectionString = ConfigurationManager.AppSettings["ConnectionString"];
			m_mysqlConnection = new GameScoreBoardMysqlConnection (m_connectionString); 
		}

		[Test()]
		[Category("integration")]
		public void AddScoreRecordToStorage_GivenValidScoreRecord_ReturnsTrue()
		{
			var exampleRecord = new ScoreRecord { GameName = "game1", PlayerName = m_playerName, Score = 1000 };

			Assert.IsTrue (m_mysqlConnection.AddScoreRecordToStorage (exampleRecord));
		}

		[Test()]
		[Category("integration")]
		public void GetAllScoresForUsername_GivenValidUsername_ReturnsScoreForUsername()
		{
			var resultFromDb = m_mysqlConnection.GetAllScoresForUsername (m_playerName); 

			Assert.IsTrue (resultFromDb.Count() > 0); 
		}

		[TearDown]
		public void CleanupPlayerName()
		{
			var sql = @"DELETE FROM GameScoreBoard WHERE PlayerName = @PlayerName"; 

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString; 
					connection.Open();
					connection.Execute(sql, new {PlayerName = m_playerName}); 

					 
				}
				catch(MySql.Data.MySqlClient.MySqlException e) 
				{
					Console.WriteLine (e.ToString ()); 
				}
				finally
				{
					connection.Close(); 
				}
			}
		}
	}
}

