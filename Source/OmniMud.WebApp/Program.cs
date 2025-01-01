using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace OmniMud.WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

			/*
			builder.Services.AddAuthentication().AddGoogle(options =>
			{
				options.ClientId = builder.Configuration.GetValue<string>("Authentication:Google:ClientId");
				options.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Google:ClientSecret");
			});
			*/
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;				
			})
			.AddCookie(options =>
			{
				options.LoginPath = "/Auth/Login";
				options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
				options.SlidingExpiration = true;
			})
			.AddOpenIdConnect(options =>
			{
				var oidcConfig = builder.Configuration.GetSection("Authentication:Google");

				options.Authority = oidcConfig["Authority"];
				options.ClientId = oidcConfig["ClientId"];
				options.ClientSecret = oidcConfig["ClientSecret"];
				
				options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.ResponseType = OpenIdConnectResponseType.Code;

				options.SaveTokens = true;
				options.GetClaimsFromUserInfoEndpoint = true;

				options.MapInboundClaims = false;
				options.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
				options.TokenValidationParameters.RoleClaimType = "roles";
			});

			/*
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/Auth/Login";
					options.ReturnUrlParameter = "ReturnUrl";
					options.LogoutPath = "/Auth/Logout";
					options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
					options.SlidingExpiration = true;
				});

			builder.Services.AddOpenIdConnect
			*/

			builder.Services.AddHttpContextAccessor();

			var app = builder.Build();

			app.UseStaticFiles();

			app.UseAuthentication();
			//app.UseAuthorization();

			app.MapDefaultControllerRoute();

			app.Run();
		}
	}
}
