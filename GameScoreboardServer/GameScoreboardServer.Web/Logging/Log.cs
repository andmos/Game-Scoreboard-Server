using System;

namespace GameScoreboardServer
{
	public class Log : ILog
	{
		private readonly Action<string> _logDebug;
		private readonly Action<string, Exception> _logError;
		private readonly Action<string> _logInfo;

		public Log(Action<string> logInfo, Action<string> logDebug, Action<string, Exception> logError)
		{
			_logInfo = logInfo;
			_logDebug = logDebug;
			_logError = logError;
		}

		public void Info(string message)
		{
			_logInfo(message);
		}

		public void Debug(string message)
		{
			_logDebug(message);
		}

		public void Error(string message, Exception exception = null)
		{
			_logError(message, exception);
		}
	}
}

