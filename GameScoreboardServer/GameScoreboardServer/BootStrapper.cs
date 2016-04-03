using System;
using System.Configuration;
using LightInject.Nancy;
using LightInject;
using Nancy.Diagnostics;

namespace GameScoreboardServer
{
	public class BootStrapper : LightInjectNancyBootstrapper
	{
		protected override DiagnosticsConfiguration DiagnosticsConfiguration 
		{
			get 
			{
				return new DiagnosticsConfiguration { Password = ConfigurationManager.AppSettings ["DiagnosticsPassword"] };
			}
		}

	}
			
}


