using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace OmniMud.WebApp.Hubs
{
	[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
	public class OmniMudHub : Hub
	{
		public async Task SendCommand(string command)
		{
			await Clients.Caller.SendAsync("Update", "Thanks, we received your command!");
		}
	}
}
