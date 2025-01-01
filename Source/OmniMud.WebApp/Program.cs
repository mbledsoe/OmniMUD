using Microsoft.AspNetCore.Authentication.Cookies;

namespace OmniMud.WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/Auth/Login";
					options.ReturnUrlParameter = "ReturnUrl";
					options.LogoutPath = "/Auth/Logout";
					options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
				});

			builder.Services.AddHttpContextAccessor();

			var app = builder.Build();

			app.UseStaticFiles();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapDefaultControllerRoute();

			app.Run();
		}
	}
}
