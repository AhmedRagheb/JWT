﻿
namespace JWTAuthentication.Models
{
	public class TokenResponse
	{
		public string token_type { get; set; }
		public string access_token { get; set; }
		public int expires_in { get; set; }
	}
}
