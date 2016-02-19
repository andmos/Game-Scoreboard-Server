using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameScoreboardServer.Services;
using GameScoreboardServer.Models;
using Nancy.ModelBinding;
using Nancy.Routing;
using GameScoreboardServer.Crypto; 

namespace GameScoreboardServer.Modules
{
    public class ScoreboardModule :  NancyModule 
    {
		private IDataStorage m_dataStorage; 
		private ICryptation m_cryptation; 
		private ILog m_logger; 

		public ScoreboardModule(IDataStorage dataStorage, ICryptation cryptation, ILogFactory logger) : base("/api/v1")
        {
			StaticConfiguration.DisableErrorTraces = false;
			m_dataStorage = dataStorage; 
			m_cryptation = cryptation; 
			m_logger = logger.GetLogger (GetType ()); 

			Get["/ping"] = parameters =>
            {
				var response = (Response) "pong"; 
				response.StatusCode = HttpStatusCode.OK;

				return response; 
            }; 
        	
			Post ["/addScoreBoardData"] = parameters => 
			{
				try 
				{
					var scoreBoardData = this.Bind<ScoreRecord> (); 
					m_dataStorage.AddScoreRecordToStorage(scoreBoardData); 
					var response = (Response) "Created"; 
					response.StatusCode = HttpStatusCode.Created; 
					return response;
				} 
				catch (Exception e) 
				{
					m_logger.Error(e.Message, e);  
					var response = (Response) e.ToString(); 
					response.StatusCode = HttpStatusCode.BadRequest; 

					return response;
				}
			};

			Get ["/gameScoreBoard"] = parameters => 
			{
				string gameNameFromQuery = Request.Query ["gameName"];
				string numberOfRecords = Request.Query["count"];
				int count; 
				if(int.TryParse(numberOfRecords, out count))
				{
					try
					{
						return Response.AsJson(m_dataStorage.GetScoresForGame(gameNameFromQuery, count)); 
					}
					catch(Exception e)
					{
						m_logger.Error(e.Message, e);
						var response = (Response) e.ToString();
						response.StatusCode = HttpStatusCode.BadRequest; 

						return response; 
					}
				}
				return Response.AsJson (m_dataStorage.GetAllScoresForGame(gameNameFromQuery)); 
			};

			Get ["/playerScoreBoard"] = parameters => 
			{
				try
				{
					string playerNameFromQuery = Request.Query ["playerName"]; 
					return Response.AsJson (m_dataStorage.GetAllScoresForUsername(playerNameFromQuery)); 	
				}
				catch(Exception e)
				{
					m_logger.Error(e.Message, e); 
					var response = (Response) e.ToString();
					response.StatusCode = HttpStatusCode.BadRequest; 

					return response; 
				}
			};
		}
    }
}
