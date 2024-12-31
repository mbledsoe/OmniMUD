using Microsoft.AspNetCore.Mvc;

namespace OmniMud.WebApp.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
