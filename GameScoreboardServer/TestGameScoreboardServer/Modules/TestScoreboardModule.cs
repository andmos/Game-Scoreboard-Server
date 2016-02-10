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
using Moq;
using GameScoreboardServer.Services;

namespace TestGameScoreboardServer.Modules
{

	[TestFixture()]
	public class TestScoreboardModule
	{
		[Test()]
		[Category("unit")]
		public void Ping_GivenCorrectRoute_ReturnsPong()
		{
			var bootstrapper = new LightInjectNancyBootstrapper();
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

			var result = browser.Get("/api/v1/ping", with =>
				{
					with.HttpRequest();
				});

			Assert.AreEqual (HttpStatusCode.OK, result.StatusCode);
			Assert.AreEqual ("pong", result.Body.AsString());
		}

		[Test()]
		[Category("unit")]
		public void GameScoreBoard_GivenValidGameNameAndCount_ReturnsCorrectJson()
		{
			var bootstrapper = new TestableLightInjectNancyBootstrapper ();
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));
			string expectedGameName = "game1";
			int expectedNumberOfResults = 1;

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
		[Category("unit")]
		public void PlayerScoreBoard_GivenValidUsername_ReturnsCorrectJson()
		{
			string expectedPlayerName = "player1";
			var mockStorage = new Mock<IDataStorage> ();
			mockStorage.Setup(x => x.GetAllScoresForUsername(expectedPlayerName)).Returns(new List<ScoreRecord>() { new ScoreRecord {PlayerName = expectedPlayerName}});
			var bootstrapper = new TestableLightInjectNancyBootstrapper(mockStorage.Object);
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));


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
		[Category("unit")]
		public void AddScoreBoardData_GivenValidGameRecordObjectAsJson_ReturnsHttpCreated()
		{
			var bootstrapper = new TestableLightInjectNancyBootstrapper ();
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

		[Test()]
		[Category("unit")]
		public void AddScoreBoardData_GivenValidGameRecordObjectAsJson_ReturnsCorrectObjectFromStorage()
		{
			var bootstrapper = new TestableLightInjectNancyBootstrapper ();
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));
			string expectedGameName = "game5";
			int expectedCount = 1;
			var scoreRecordObject = new ScoreRecord {
				GameName = expectedGameName,
				PlayerName = expectedCount.ToString(),
				Score = 5000
			};

			var post = browser.Post ("/api/v1/addScoreBoardData", with => {
				with.Header("Content-Type", "application/json");
				with.JsonBody (scoreRecordObject);
			});

			var response = browser.Get("/api/v1/gameScoreBoard", with =>
				{
					with.HttpRequest();
					with.Query("gameName", expectedGameName);
					with.Query("count", expectedCount.ToString());
				});
			var responseModels = JsonConvert.DeserializeObject<IEnumerable<ScoreRecord>> (response.Body.AsString());

			Assert.IsTrue (responseModels.Count() == expectedCount);
			Assert.AreEqual (expectedGameName, responseModels.FirstOrDefault ().GameName);


		}



	}

}