using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightInject;
using GameScoreboardServer.Services;
using System.Configuration;
using GameScoreboardServer.Crypto; 

namespace GameScoreboardServer
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
			var dataStorage = ConfigurationManager.AppSettings["DataStorage"];
			if (dataStorage.ToLower ().Equals ("cache")) 
			{
				serviceRegistry.Register<IDataStorage, GameScoreBoardDataCache> (); 
			}
			if (dataStorage.ToLower ().Equals ("database")) 
			{
				var mysqlConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
				serviceRegistry.Register<IDataStorage> (factory => new GameScoreBoardMysqlConnection (mysqlConnectionString));
			}
			serviceRegistry.Register<ICryptation, StringCipher> (); 
			serviceRegistry.Register<ILogFactory, Log4NetLogFactory>(new PerContainerLifetime());
			serviceRegistry.Register<Type, ILog>((factory, type) => factory.GetInstance<ILogFactory>().GetLogger(type));
			serviceRegistry.RegisterConstructorDependency(
				(factory, info) => factory.GetInstance<Type, ILog>(info.Member.DeclaringType));   
			serviceRegistry.Decorate<IDataStorage, DataStorageProfiler> ();
        }
    }
}
