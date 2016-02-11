using System;

namespace GameScoreboardServer
{
	public interface ILogFactory
	{
		ILog GetLogger(Type type);
	}
}

