using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using JWTAuthentication.Db;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TokenOptions = JWTAuthentication.Models.TokenOptions;

namespace JWTAuthentication.Controllers
{
	[Route("api/Account")]
	public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
	    private TokenOptions Options { get; }

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<TokenOptions> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
	        Options = options.Value;
		}

		[HttpPost]
		[Route("Register")]
		public IActionResult Register([FromBody]User user)
        {
			if (user.Password != user.Repassword)
			{
				return BadRequest("Password don't match");
			}

			var newUser = new ApplicationUser 
            {
                UserName = user.Email,
                Email = user.Email
			};

            var userCreationResult = _userManager.CreateAsync(newUser, user.Password);
			if (!userCreationResult.Result.Succeeded)
			{
				return BadRequest(userCreationResult.Result.Errors);
			}

			_userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Role, "Administrator"));

	        return Ok(newUser);

        }
     

        [HttpPost]
        [Route("Login")]
		public IActionResult Login([FromBody]LogInModel model)
        {
            var user =  _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return BadRequest("Invalid login");
			}

	        var token = new JwtSecurityToken(
		        audience: Options.Audience,
		        issuer: Options.Issuer,
		        expires: Options.GetExpiration(),
		        signingCredentials: Options.GetSigningCredentials());

			var tokenR = new TokenResponse
	        {
				token_type = Options.Type,
		        access_token = new JwtSecurityTokenHandler().WriteToken(token),
		        expires_in = (int)Options.ValidFor.TotalSeconds
			};

			return Ok(new {user, tokenR});
		}

		
	    public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
	        return Ok();
        }                    
    }
}
