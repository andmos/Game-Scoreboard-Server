using GameScoreboardServer.Models;
using GameScoreboardServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestGameScoreBoardServer.Tests.Services
{
    public class TestGameScoreBoardDataCache
    {
        [Fact]       
        public void AddScoreRecordToStorage_GivenGameNameAndSingleScoreRecord_RecordGetsAddedToCache()
        {
            var cache = new GameScoreBoardDataCache();
            var record = new GameScoreRecord { PlayerName = "Player1", Score = 3000 };
            string gameName = "Game1";

            cache.AddScoreRecordToStorage(gameName, record);

            Assert.Equal(cache.GetAllScoresForGame(gameName).FirstOrDefault().PlayerName, record.PlayerName); 

        }
    }
}
