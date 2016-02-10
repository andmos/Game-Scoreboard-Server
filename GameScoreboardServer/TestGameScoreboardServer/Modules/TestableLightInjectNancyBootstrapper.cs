using System;
using LightInject.Nancy;
using LightInject;
using GameScoreboardServer;
using GameScoreboardServer.Services;
using GameScoreboardServer.Models;
using System.Collections.Generic;
using GameScoreboardServer.Crypto;

namespace TestGameScoreboardServer
{
	public class TestableLightInjectNancyBootstrapper : LightInjectNancyBootstrapper 
	{

		IDataStorage m_dataStorage; 

		public TestableLightInjectNancyBootstrapper(IDataStorage dataStorage = null)
		{
			if (dataStorage != null) 
			{
				m_dataStorage = dataStorage; 
			} else 
			{
				m_dataStorage = new GameScoreBoardDataCache (); 
				AddMultipleRecordsToCache (m_dataStorage); 
			}
		}

		protected override IServiceContainer GetServiceContainer()
		{

			IServiceContainer container = new ServiceContainer (); 
			container.RegisterInstance<IDataStorage> (m_dataStorage); 
			container.Register<ICryptation, StringCipher> (); 

			return container; 
		}

		private void AddMultipleRecordsToCache(IDataStorage cache)
		{
			var records = new List<ScoreRecord> () 
			{
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 5000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 4000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 6000 }, 
				new ScoreRecord { GameName = "game1", PlayerName = "player1", Score = 3000 }, 
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
				cache.AddScoreRecordToStorage(record); 
			}
		}
	}
}

