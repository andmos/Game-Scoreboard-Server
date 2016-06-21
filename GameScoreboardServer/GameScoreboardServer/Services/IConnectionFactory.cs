using System;
using System.Data;

namespace GameScoreboardServer
{
	public interface IConnectionFactory
	{
		IDbConnection GetOpenConnection(); 
	}
}

