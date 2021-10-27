using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using GameScoreboardServer.Web.Models;
using MySql.Data.MySqlClient;

namespace GameScoreboardServer.Web.Services
{
	public class GameScoreBoardMysqlRepository : IScoreBoardRepository 
	{
		private readonly IConnectionFactory m_connectionFactory; 

		public GameScoreBoardMysqlRepository (IConnectionFactory connectionFactory)
		{
			m_connectionFactory = connectionFactory; 
		}
			

		public IEnumerable<ScoreRecord> GetAllScoresForGame (string gameName)
		{
			var sql = @"Select GameName, PlayerName, Score, recordId FROM GameScoreBoard WHERE gameName = @GameName";

			using (var connection = m_connectionFactory.GetOpenConnection()) 
			{
				try
				{
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

			using (var connection = m_connectionFactory.GetOpenConnection())
			{
				try
				{
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

			using (var connection = m_connectionFactory.GetOpenConnection())
			{
				try
				{
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

			using (var connection = m_connectionFactory.GetOpenConnection())
			{
				try
				{
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


		public ScoreRecord GetScoreRecordForUsername(int recordId, string playerName)
		{
			throw new NotImplementedException();
		}

		public int CountHigherScores(string gameName, int score)
		{
			var sql = @"Select COUNT(*) FROM GameScoreBoard WHERE GameName = @GameName AND Score > @Score"; 
		
			using (var connection = m_connectionFactory.GetOpenConnection())
			{
				try
				{
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

		public IEnumerable<string> GetAllGameNames()
		{
			var sql = @"SELECT DISTINCT GameName FROM GameScoreBoard";

			using (var connection = m_connectionFactory.GetOpenConnection()) 
			{
				try
				{
					IEnumerable<string> result = connection.Query<string>(sql);
					return result;
				}

				catch (MySqlException e)
				{
					Console.WriteLine(e);
					return Enumerable.Empty<string>();
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

			using (var connection = m_connectionFactory.GetOpenConnection())
			{
				try
				{
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

