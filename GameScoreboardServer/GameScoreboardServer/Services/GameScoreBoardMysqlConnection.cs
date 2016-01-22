using System;
using GameScoreboardServer.Services;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GameScoreboardServer.Models;
using System.Linq;
using Dapper;

namespace GameScoreboardServer
{
	public class GameScoreBoardMysqlConnection : IDataStorage 
	{
		private readonly string m_connectionString; 

		public GameScoreBoardMysqlConnection (string connecetionString)
		{
			m_connectionString = connecetionString; 
		}
			

		public IEnumerable<ScoreRecord> GetAllScoresForGame (string gameName)
		{
			var sql = @"Select * FROM GameScoreBoard WHERE gameName = @GameName";

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString; 
					connection.Open();
					IEnumerable<ScoreRecord> result = connection.Query<ScoreRecord>(sql, new {GameName = gameName}); 

					return result; 
				}

				catch(MySql.Data.MySqlClient.MySqlException e) 
				{
					Console.WriteLine (e.ToString ());
					return Enumerable.Empty<ScoreRecord>(); 
				}
				finally
				{
					connection.Close(); 
				}
			}
		}

		public IEnumerable<ScoreRecord> GetTopTenScoresForGame(string gameName)
		{
			var sql = @"Select * FROM GameScoreBoard WHERE gameName = @GameName ORDER BY GameScoreBoard.Score DESC limit 10";

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString; 
					connection.Open();
					IEnumerable<ScoreRecord> result = connection.Query<ScoreRecord>(sql, new {GameName = gameName}); 

					return result; 
				}

				catch(MySql.Data.MySqlClient.MySqlException e) 
				{
					Console.WriteLine (e.ToString ());
					return Enumerable.Empty<ScoreRecord>(); 
				}
				finally
				{
					connection.Close(); 
				}
			}
		}

		public IEnumerable<ScoreRecord> GetAllScoresForUsername (string username)
		{
			var sql = @"Select * FROM GameScoreBoard WHERE PlayerName = @PlayerName"; 

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString; 
					connection.Open();
					IEnumerable<ScoreRecord> result = connection.Query<ScoreRecord>(sql, new {PlayerName = username}); 

					return result; 
				}

				catch(MySql.Data.MySqlClient.MySqlException e) 
				{
					Console.WriteLine (e.ToString ());
					return Enumerable.Empty<ScoreRecord>(); 
				}
				finally
				{
					connection.Close(); 
				}
			}
		}

		public bool AddScoreRecordToStorage (ScoreRecord record)
		{
			var sql = @"INSERT INTO GameScoreBoard(GameName,PlayerName,Score) VALUES (@GameName, @PlayerName, @Score);"; 

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString;
					connection.Open(); 
					connection.Execute(sql, record);
					return true; 

				}
				catch(MySql.Data.MySqlClient.MySqlException e)
				{
					Console.WriteLine (e.ToString ());
					return false; 
				}
				finally
				{
					connection.Close();
				}
			}
		}
			
	}
}

