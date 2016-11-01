using System;
using GameScoreboardServer.Services;
using GameScoreboardServer.Models;
using System.Collections.Generic;

namespace TestGameScoreboardServer
{
	public static class TestDataProvider
	{
		
		public static void ProvideTestData(IDataStorage dataStorage)
		{
			var records = new List<ScoreRecord> () 
			{
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 5000 }, 
				new ScoreRecord { GameName = "game2", PlayerName = "player1", Score = 4000 }, 
				new ScoreRecord { GameName = "game3", PlayerName = "player1", Score = 6000 }, 
				new ScoreRecord { GameName = "game3", PlayerName = "player1", Score = 3000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 8000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 7000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 9000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 8000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 4000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 7000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 8000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 8000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 9000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player2", Score = 11000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player3", Score = 1000 }, 
			};
			foreach (var record in records) 
			{
				dataStorage.AddScoreRecordToStorage(record); 
			}
		}
	}
}

