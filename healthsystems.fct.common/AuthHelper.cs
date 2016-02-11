using System;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace healthsystems.fct.common
{
    /// <summary>
    /// Description of AuthHelper.
    /// </summary>
    public static class AuthHelper
    {
        public static int JwtAuth()
        {
            // is there an auth header?
            var AuthHeader = HttpContext.Current.Request.Headers["Authorization"];
            if (AuthHeader == null) return 2;

            // no - return un-authorized
            // 401 Unauthorized 

            // yes - split by space ' '.
            var SplitAuth = AuthHeader.Split(new[] { " " }, StringSplitOptions.None);

            // is splits count = 2?
            if (SplitAuth.Length != 2) return 3;

            // no - invalid auth header sent
            // 401 Unauthorized 

            // yes - is 1st = Bearer ?
            if (!SplitAuth[0].Equals("Bearer")) return 4;

            // no - invalid auth header sent
            // 401 Unauthorized 

            // yes - store 2nd split as jwtToken
            var jwtToken = SplitAuth[1];

            // validate token. is token valid?
            if (!JwtHelper.IsTokenValid(jwtToken)) return 5;

            // no - return un-authorized
            // 401 Unauthorized

            // yes - continue w/ method
            return 1;
        }


		public static string GetKey(string tokenKey)
		{
				try{

					// do we have an auth header?
					var AuthHeader = HttpContext.Current.Request.Headers["Authorization"];
					if(AuthHeader == null) return "";

					var SplitAuth = AuthHeader.Split(new[] {" "}, StringSplitOptions.None);

					if(SplitAuth.Length != 2) return "";

					if(!SplitAuth[0].Equals("Bearer")) return "";

					var jwtToken = SplitAuth[1];

					if(!JwtHelper.IsTokenValid(jwtToken)) return "";

					var decodedJwt = JwtHelper.Decode(jwtToken);

					var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedJwt);


					return values[tokenKey];

				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
		}
}