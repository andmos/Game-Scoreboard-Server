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
			var sql = @"Select GameName, PlayerName, Score, recordId FROM GameScoreBoard WHERE gameName = @GameName";

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString; 
					connection.Open();
					IEnumerable<ScoreRecord> result = connection.Query<ScoreRecord>(sql, new {GameName = gameName}); 

					return result; 
				}

				catch(MySqlException e) 
				{
					Console.WriteLine (e);
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
			var sql = @"Select GameName, PlayerName, Score, recordId FROM GameScoreBoard WHERE gameName = @GameName ORDER BY GameScoreBoard.Score DESC limit 10";

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString; 
					connection.Open();
					IEnumerable<ScoreRecord> result = connection.Query<ScoreRecord>(sql, new {GameName = gameName}); 

					return result; 
				}

				catch(MySqlException e) 
				{
					Console.WriteLine (e);
					return Enumerable.Empty<ScoreRecord>(); 
				}
				finally
				{
					connection.Close(); 
				}
			}
		}

		public IEnumerable<ScoreRecord> GetScoresForGame (string gameName, int numberOfScores)
		{
			if (numberOfScores > 50) 
			{
				throw new ArgumentOutOfRangeException (); 
			}
			var sql = @"Select GameName, PlayerName, Score, recordId FROM GameScoreBoard WHERE gameName = @GameName ORDER BY GameScoreBoard.Score DESC limit @NumberOfScores";

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString; 
					connection.Open();
					IEnumerable<ScoreRecord> result = connection.Query<ScoreRecord>(sql, new {GameName = gameName, NumberOfScores = numberOfScores});

					return result; 
				}

				catch(MySqlException e) 
				{
					Console.WriteLine (e);
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
			var sql = @"Select GameName, PlayerName, Score, recordId FROM GameScoreBoard WHERE PlayerName = @PlayerName"; 

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString; 
					connection.Open();
					IEnumerable<ScoreRecord> result = connection.Query<ScoreRecord>(sql, new {PlayerName = username}); 

					return result; 
				}

				catch(MySqlException e) 
				{
					Console.WriteLine (e);
					return Enumerable.Empty<ScoreRecord>(); 
				}
				finally
				{
					connection.Close(); 
				}
			}
		}

		public int CountHigherScores(string gameName, int score)
		{
			var sql = @"Select COUNT(*) FROM GameScoreBoard WHERE GameName = @GameName AND Score > @Score"; 
		
			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString; 
					connection.Open();
					return connection.Query<int>(sql, new {GameName = gameName, Score = score}).FirstOrDefault(); 
					 
				}

				catch(MySqlException e) 
				{
					Console.WriteLine (e);
					return -1; 
				}
				finally
				{
					connection.Close(); 
				}
			}
		}

		public int AddScoreRecordToStorage (ScoreRecord record)
		{
			var sql = @"INSERT INTO GameScoreBoard(GameName,PlayerName,Score) VALUES (@GameName, @PlayerName, @Score);
			SELECT LAST_INSERT_ID();"; 

			using (var connection = new MySqlConnection ()) 
			{
				try
				{
					connection.ConnectionString = m_connectionString;
					connection.Open(); 
					return connection.Query<int>(sql, record).FirstOrDefault(); 

				}
				catch(MySqlException e)
				{
					Console.WriteLine (e);
					return -1; 
				}
				finally
				{
					connection.Close();
				}
			}
		}
			
	}
}

