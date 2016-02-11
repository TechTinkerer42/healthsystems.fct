using System.Collections.Generic;
using System.Web;
using System;

namespace healthsystems.fct.common
{
	/// <summary>
	/// Description of JwtHelper.
	/// </summary>
	public static class JwtHelper
	{
		private const string JwtSecret = "dgfsagdsfg/34wefedf";
		
		public static string Encode(int UserId, string Username, int StateId)
		{
		
			var payload = new Dictionary<string, string>()
			{
				{ "userId", UserId.ToString() },
				{ "username", Username },
				{ "stateId", StateId.ToString() }
			};
			
			var token = JWT.JsonWebToken.Encode(payload, JwtSecret, JWT.JwtHashAlgorithm.HS256);
			
			return token;
		}
		
		public static string Decode(string jwtHash)
		{
			try
			{
    			return JWT.JsonWebToken.Decode(jwtHash, JwtSecret);
			}
			catch (JWT.SignatureVerificationException)
			{
				throw;
			}
		}
		
		public static bool IsTokenValid(string jwtHash)
		{
			try
			{
    			JWT.JsonWebToken.Decode(jwtHash, JwtSecret);
    			return true;
			}
			catch (JWT.SignatureVerificationException)
			{
				return false;
			}
		}
		
	}
}

