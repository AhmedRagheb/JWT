using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public class User
    {
	    public string Email { get; set; }
	    public string Password { get; set; }
	    public string Repassword { get; set; }

	}

	public class LogInModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
