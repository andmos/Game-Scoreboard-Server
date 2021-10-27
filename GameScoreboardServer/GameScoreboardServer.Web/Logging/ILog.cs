﻿using System;

namespace GameScoreboardServer
{
	public interface ILog
	{      
		void Info(string message);

		void Debug(string message);

		void Error(string message, Exception exception = null);
	}

}

