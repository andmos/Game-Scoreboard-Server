using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace GameScoreboardServer.Web.Services
{
	public class MySqlConnectionFactory : IConnectionFactory
	{
		private readonly string m_connectionString;
		public MySqlConnectionFactory(string connectionString)
		{
			m_connectionString = connectionString; 
		}

		public IDbConnection GetOpenConnection()
		{
			var connection = new MySqlConnection(m_connectionString);
			connection.Open();
			return connection;
		}

	}
}

