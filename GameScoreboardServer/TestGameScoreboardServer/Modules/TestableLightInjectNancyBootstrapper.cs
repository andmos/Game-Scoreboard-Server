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

		public TestableLightInjectNancyBootstrapper(IDataStorage dataStorage)
		{
			if (dataStorage != null) 
			{
				m_dataStorage = dataStorage; 
			} else 
			{
				m_dataStorage = new GameScoreBoardDataCache (); 
			}
		}

		protected override IServiceContainer GetServiceContainer()
		{

			IServiceContainer container = new ServiceContainer (); 
			container.RegisterInstance<IDataStorage> (m_dataStorage); 
			container.Register<ICryptation, StringCipher> (); 

			return container; 
		}
	}
}

