using System;

namespace GameScoreboardServer.Web.Crypto
{
	public interface ICryptation
	{
		string Encrypt (string plainText, string passPhrase); 
		string Decrypt (string cipherText, string passPhrase); 
	}
}

