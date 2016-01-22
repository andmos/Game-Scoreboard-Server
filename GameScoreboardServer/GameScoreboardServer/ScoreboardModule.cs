using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameScoreboardServer.Services;
using GameScoreboardServer.Models;
using Nancy.ModelBinding;

namespace GameScoreboardServer
{
    public class ScoreboardModule :  NancyModule 
    {
		private IDataStorage m_dataStorage; 

		public ScoreboardModule(IDataStorage dataStorage) : base("/api/v1")
        {
			StaticConfiguration.DisableErrorTraces = false;
			m_dataStorage = dataStorage; 

			Get["/ping"] = parameters =>
            {
                return "pong";
            }; 
        
			Post ["/addScoreBoardData"] = parameters => 
			{
				try 
				{
					var scoreBoardData = this.Bind<ScoreRecord> (); 
					m_dataStorage.AddScoreRecordToStorage(scoreBoardData); 
					return HttpStatusCode.Created; 
				} 
				catch (Exception e) 
				{
					Console.WriteLine(e.Message); 
					return HttpStatusCode.InternalServerError; 
				}
			};

			Get ["/gameScoreBoard"] = parameters => 
			{
				string gameNameFromQuery = Request.Query ["gameName"];
				return Response.AsJson (m_dataStorage.GetAllScoresForGame(gameNameFromQuery)); 
			};

			Get ["/gameScoreBoardTopTen"] = parameters => 
			{
				string gameNameFromQuery = Request.Query ["gameName"];
				return Response.AsJson (m_dataStorage.GetTopTenScoresForGame(gameNameFromQuery)); 
			};

			Get ["/gameScoreBoard"] = parameters => 
			{
				string playerNameFromQuery = Request.Query ["playerName"]; 
				return Response.AsJson (m_dataStorage.GetAllScoresForUsername(playerNameFromQuery)); 
			};
		}
    }
}
