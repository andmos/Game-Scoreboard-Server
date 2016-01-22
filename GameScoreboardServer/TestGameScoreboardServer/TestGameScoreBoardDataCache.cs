using GameScoreboardServer.Models;
using GameScoreboardServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace TestGameScoreBoardServer.Tests.Services
{
	[TestFixture ()]
	public class TestGameScoreBoardDataCache 
	{     
		[Test()]
		[Category("unit")]
		public void AddScoreRecordToStorage_GivenGameNameAndSingleScoreRecord_RecordGetsAddedToCache()
		{
			var cache = new GameScoreBoardDataCache();
			var record = new ScoreRecord { PlayerName = "Player1", Score = 3000, GameName = "game1"};
			string gameName = "Game1";

			cache.AddScoreRecordToStorage(record);

			Assert.AreEqual(cache.GetAllScoresForGame(gameName).FirstOrDefault().PlayerName, record.PlayerName);
		}
	}
}
