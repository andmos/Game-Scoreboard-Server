using System.Data;

namespace GameScoreboardServer.Web.Services
{
	public interface IConnectionFactory
	{
		IDbConnection GetOpenConnection(); 
	}
}

