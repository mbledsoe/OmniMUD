using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using OmniMud.WebApp.Models.Auth;

namespace OmniMud.WebApp.Controllers
{
	public class AuthController : Controller
	{
		private readonly IConfiguration configuration;

		public AuthController(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			var vm = new AuthLoginView { ReturnUrl = returnUrl };
			
			return View(vm);
		}

		[HttpGet]
		public async Task GoogleLogin(string returnUrl)
		{
			var authProperties = new AuthenticationProperties
			{
				IsPersistent = false,
				RedirectUri = returnUrl
			};

			await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, authProperties);
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}
