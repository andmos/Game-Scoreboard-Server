using System;
using NUnit.Framework;
using Moq;
using System.Linq;
using GameScoreboardServer;
using GameScoreboardServer.Services;
using System.Collections.Generic;
using GameScoreboardServer.Models;

namespace TestGameScoreboardServer
{
	[TestFixture()]
	public class TestDataStorageProfiler
	{
		private Mock<ILog> m_mockLogger; 
		private IDataStorage m_mockDataStorage;

		public TestDataStorageProfiler ()
		{
			m_mockLogger = new Mock<ILog> ();
			var storageMock = new Mock<IDataStorage> ();
			storageMock.Setup (m => m.GetAllScoresForGame ("game1")).Returns (new List<ScoreRecord>() { new ScoreRecord {PlayerName = "player1"}});
			m_mockDataStorage = storageMock.Object;

		}

		[Test]
		[Category("unit")]
		public void GetAllScoresForGame_GivenDataStorageImplementation_LoggerGetsCalled()
		{
			var dataStorageProfiler = new DataStorageProfiler (m_mockDataStorage, m_mockLogger.Object);

			dataStorageProfiler.GetAllScoresForGame ("game1");

			m_mockLogger.Verify (x => x.Info(It.IsAny<string>()), Times.AtLeastOnce ());
		}
	
	}
}

