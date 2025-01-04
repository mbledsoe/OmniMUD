using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OmniMud.Core;
using OmniMud.Core.Commands;

namespace OmniMud.WebApp.Hubs
{
	[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
	public class OmniMudHub : Hub
	{
		private readonly IOmniMudGame omniMudGame;
		private readonly IInputQueue inputQueue;

		public OmniMudHub(IOmniMudGame omniMudGame,
			IInputQueue inputQueue)
		{
			this.omniMudGame = omniMudGame;
			this.inputQueue = inputQueue;
		}

		public override Task OnConnectedAsync()
		{
			omniMudGame.EnterGame(Context.ConnectionId);
			return base.OnConnectedAsync();
		}

		public async Task SendCommand(string command)
		{
			var playerInput = new PlayerInput
			{
				ConnectionId = Context.ConnectionId,
				RawCommand = command.Trim()
			};

			inputQueue.Enqueue(playerInput);
		}
	}
}
