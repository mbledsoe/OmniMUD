using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OmniMud.Core;
using OmniMud.Core.Commands;
using OmniMud.Core.World;
using OmniMud.WebApp.HostedServices;
using OmniMud.WebApp.Hubs;
using OmniMud.WebApp.MockContent;

namespace OmniMud.WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

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

			builder.Services.AddSignalR();

			builder.Services.AddHttpContextAccessor();

			// game services
			builder.Services.AddSingleton<IWorldDataService>(provider =>
			{
				var rooms = new MockWorldDataLoader().LoadWorldData();
				return new WorldDataService(rooms.First().Key, rooms);
			});

			builder.Services.AddSingleton<ICharacterManager, CharacterManager>();
			
			builder.Services.AddSingleton<IInputQueue, InputQueue>();
			builder.Services.AddSingleton<IInputProcessor, InputProcessor>();

			builder.Services.AddSingleton<IOutputQueue, OutputQueue>();
			builder.Services.AddScoped<IOutputProcessor, SignalROutputProcessor>();
			
			builder.Services.AddSingleton<IOmniMudGame, OmniMudGame>();
			builder.Services.AddHostedService<OmniMudHostedService>();

			var app = builder.Build();

			app.UseStaticFiles();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapHub<OmniMudHub>("/OmniMudHub");

			app.MapDefaultControllerRoute();

			app.Run();
		}
	}
}
