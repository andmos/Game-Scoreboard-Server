using System;

namespace GameScoreboardServer.Crypto
{
	public interface ICryptation
	{
		string Encrypt (string plainText, string passPhrase); 
		string Decrypt (string cipherText, string passPhrase); 
	}
}

