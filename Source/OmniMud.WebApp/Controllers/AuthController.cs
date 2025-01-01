using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmniMud.WebApp.Models;

namespace OmniMud.WebApp.Controllers
{
	[Authorize]
	public class AuthController : Controller
	{
		private readonly IHttpContextAccessor httpContextAccessor;

		public AuthController(IHttpContextAccessor httpContextAccessor)
		{
			this.httpContextAccessor = httpContextAccessor;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Login(string returnUrl)
		{
			var loginPostRequest = new LoginPostRequest
			{
				ReturnUrl = returnUrl,
			};

			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Login(LoginPostRequest request)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			// Validate username/password here.  This is a temporary placeholder.
			var testUserId = Guid.NewGuid();
			var testUserName = $"TestUser{testUserId}";
			var testEmail = $"{testUserName}@testdomain.com";

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, testUserId.ToString()),
				new Claim(ClaimTypes.Name, testUserName),
				new Claim(ClaimTypes.Email, testEmail)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{				
				IssuedUtc = DateTimeOffset.UtcNow,
				IsPersistent = false
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
				new ClaimsPrincipal(claimsIdentity), 
				authProperties);

			return Redirect(request.ReturnUrl);
		}
	}
}
