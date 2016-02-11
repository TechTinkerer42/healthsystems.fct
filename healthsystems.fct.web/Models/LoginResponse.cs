using System;

namespace healthsystems.fct.web
{
	public class LoginResponse
	{
		public int StatusCode { get; set; }
		public string Reason { get; set; }
		public string Username { get; set; }
		public string Token { get; set; }
	}
}

