using Microsoft.AspNetCore.SignalR;
using OmniMud.Core.Commands;

namespace OmniMud.WebApp.Hubs
{
	public class SignalROutputProcessor : IOutputProcessor
	{
		private readonly IHubContext<OmniMudHub> hub;

		public SignalROutputProcessor(IHubContext<OmniMudHub> hub)
		{
			this.hub = hub;
		}

		public async Task ProcessOutput(PlayerOutput output)
		{
			await hub.Clients.Client(output.ConnectionId).SendAsync("Update", output.Message);
		}
	}
}
