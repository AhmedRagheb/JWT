using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
	[Route("[controller]")]
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}