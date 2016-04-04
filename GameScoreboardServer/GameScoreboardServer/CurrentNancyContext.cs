using System;
using Nancy.Bootstrapper;
using LightInject;
using Nancy;

namespace GameScoreboardServer
{
	public class CurrentNancyContext :  IRequestStartup
	{
		private static LogicalThreadStorage<NancyContextStorage> messageStorage =
			new LogicalThreadStorage<NancyContextStorage>(() => new NancyContextStorage());


		public void Initialize(IPipelines pipelines, NancyContext context)
		{
			messageStorage.Value.Context = context;
		}

		public static NancyContext GetCurrentContext()
		{
			return messageStorage.Value.Context;
		}
	}


	public class NancyContextStorage
	{
		public NancyContext Context { get; set; }
	}
}