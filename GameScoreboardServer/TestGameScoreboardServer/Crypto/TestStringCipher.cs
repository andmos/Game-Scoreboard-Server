using System;
using NUnit.Framework;
using GameScoreboardServer.Crypto;
using GameScoreboardServer.Models;
using Newtonsoft.Json; 

namespace TestGameScoreboardServer
{
	[TestFixture]
	public class TestStringCipher
	{
		private const string EncryptionKey = "0611a253-2056-416b-acd7-288397d26bc9"; 
		private StringCipher m_chiper; 
		private ScoreRecord m_scoreRecord; 

		public TestStringCipher ()
		{
			m_chiper = new StringCipher (); 
			m_scoreRecord = new ScoreRecord { PlayerName = "player1", GameName = "Game1", Score = 5000 }; 
		}

		[Test]
		[Category("unit")]
		public void Encrypy_GivenValidJsonModel_ReturnsEncryptetModel()
		{
			var jsonString = JsonConvert.SerializeObject (m_scoreRecord);

			var encryptet =  m_chiper.Encrypt(jsonString, EncryptionKey); 

			Assert.AreNotEqual (jsonString, encryptet); 
		}

		[Test]
		[Category("unit")]
		public void Decrypt_GivenValidEncryptetJsonModel_ReturnsDecryptedModel()
		{
			var jsonString = JsonConvert.SerializeObject (m_scoreRecord);
			var encryptet =  m_chiper.Encrypt(jsonString, EncryptionKey); 

			var decrypted = m_chiper.Decrypt (encryptet, EncryptionKey); 


			Assert.AreEqual (jsonString, decrypted); 
		}
	
	}
}

