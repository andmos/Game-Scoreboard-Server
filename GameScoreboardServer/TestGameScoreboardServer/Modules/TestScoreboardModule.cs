using System;
using System.Linq;
using NUnit.Framework;
using Nancy;
using LightInject.Nancy;
using Nancy.Testing;
using LightInject;
using Newtonsoft.Json; 
using GameScoreboardServer.Crypto;
using System.Collections.Generic;
using GameScoreboardServer.Models;

namespace TestGameScoreboardServer.Modules
{

	/*
		Tests are now using database, need to mock this shit yo! 
		Tagged as integration until then
	*/

	[TestFixture()]
	public class TestScoreboardModule
	{
		[Test()]
		[Category("unit")]
		public void Ping_GivenCorrectRoute_ReturnsPong()
		{ 
			var bootstrapper = new LightInjectNancyBootstrapper ();
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json")); 

			var result = browser.Get("/api/v1/ping", with => 
				{
					with.HttpRequest();
				});
			
			Assert.AreEqual (HttpStatusCode.OK, result.StatusCode); 
			Assert.AreEqual ("pong", result.Body.AsString());
		}
		
		[Test()]
		[Category("integration")]
		public void GameScoreBoard_GivenValidGameNameAndCount_ReturnsCorrectJson()
		{
			var bootstrapper = new LightInjectNancyBootstrapper ();
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json")); 
			string expectedGameName = "game1"; 
			int expectedNumberOfResults = 5; 

			var response = browser.Get("/api/v1/gameScoreBoard", with => 
				{
					with.HttpRequest();
					with.Query("gameName", expectedGameName);
					with.Query("count", expectedNumberOfResults.ToString()); 
				});
			var responseModels = JsonConvert.DeserializeObject<IEnumerable<ScoreRecord>> (response.Body.AsString());

			Assert.IsTrue (responseModels.Count() == expectedNumberOfResults);
			Assert.AreEqual (expectedGameName, responseModels.FirstOrDefault ().GameName); 
		}

		[Test()]
		[Category("integration")]
		public void PlayerScoreBoard_GivenValidUsername_ReturnsCorrectJson()
		{
			var bootstrapper = new LightInjectNancyBootstrapper ();
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json")); 
			string expectedPlayerName = "player1"; 

			var response = browser.Get("/api/v1/playerScoreBoard", with => 
				{
					with.HttpRequest();
					with.Query("playerName", expectedPlayerName); 
				});
			var responseModels = JsonConvert.DeserializeObject<IEnumerable<ScoreRecord>> (response.Body.AsString());

			Assert.IsTrue (responseModels.Count() >= 1); 
			Assert.AreEqual (expectedPlayerName, responseModels.FirstOrDefault().PlayerName); 
		}


		[Test()]
		[Category("integration")]
		public void AddScoreBoardData_GivenValidGameRecordObjectAsJson_ReturnsHttpCreated()
		{
			var bootstrapper = new LightInjectNancyBootstrapper ();
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json")); 
			var scoreRecordObject = new ScoreRecord {
				GameName = "game5",
				PlayerName = "player5",
				Score = 5000
			}; 

			var response = browser.Post ("/api/v1/addScoreBoardData", with => {
				with.Header("Content-Type", "application/json");
				with.JsonBody (scoreRecordObject); 
			});

			Assert.AreEqual (HttpStatusCode.Created, response.StatusCode); 
		
		}


	
	}
		
}

