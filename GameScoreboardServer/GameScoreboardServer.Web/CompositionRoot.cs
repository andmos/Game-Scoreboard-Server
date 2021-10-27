using System;
using LightInject;
using System.Configuration;
using GameScoreboardServer.Web.Services;
using GameScoreboardServer.Web.Crypto;
using GameScoreboardServer.Web.Logging;

namespace GameScoreboardServer.Web
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
			//var dataStorage = ConfigurationManager.AppSettings["DataStorage"];
			//if (dataStorage.ToLower ().Equals ("cache"))
			//{
				serviceRegistry.RegisterSingleton<IScoreBoardRepository, GameScoreBoardDataCache> (); 
			//}
			//else
			//{
			//	var mysqlConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
			//	serviceRegistry.Register<IConnectionFactory>(factory => new MySqlConnectionFactory(mysqlConnectionString));
			//	serviceRegistry.Register<IScoreBoardRepository>(factory => new GameScoreBoardMysqlRepository(factory.GetInstance<IConnectionFactory>()));
			//}
			serviceRegistry.Register<ICryptation, StringCipher> (); 
			serviceRegistry.Register<ILogFactory, SerilogFactory>(new PerContainerLifetime());
			serviceRegistry.Register<Type, ILog>((factory, type) => factory.GetInstance<ILogFactory>().GetLogger(type));
			serviceRegistry.RegisterConstructorDependency(
				(factory, info) => factory.GetInstance<Type, ILog>(info.Member.DeclaringType));   
			serviceRegistry.Decorate<IScoreBoardRepository, DataStorageProfiler> ();
        }
    }
}
