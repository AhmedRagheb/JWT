using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public class TokenOptions
    {
	    public string Type { get; set; } = "Bearer";
	    public TimeSpan ValidFor { get; set; } = TimeSpan.FromHours(1);
	    public string Audience { get; set; } = "http://localhost:60096";
		public string Issuer { get; set; } = "ConductOfCode";
	    public string SigningKey { get; set; } = "cc4435685b40b2e9ddcb357fd79423b2d8e293b897d86f5336cb61c5fd31c9a3";
    }
}
