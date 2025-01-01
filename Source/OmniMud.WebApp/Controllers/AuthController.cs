using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace OmniMud.WebApp.Controllers
{
	public class AuthController : Controller
	{
		[HttpGet]
		public async Task Login(string returnUrl)
		{
			var authProperties = new AuthenticationProperties
			{
				IsPersistent = false,
				RedirectUri = returnUrl
			};

			await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, authProperties);
		}
	}
}
