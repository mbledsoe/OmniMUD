
using OmniMud.Core;
using OmniMud.Core.Commands;

namespace OmniMud.WebApp.HostedServices
{
	public class OmniMudHostedService : BackgroundService
	{
		private readonly IServiceProvider serviceProvider;
		private readonly IOmniMudGame omniMudGame;

		public OmniMudHostedService(IServiceProvider serviceProvider,
			IOmniMudGame omniMudGame)
		{
			this.serviceProvider = serviceProvider;
			this.omniMudGame = omniMudGame;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				using (var scope = serviceProvider.CreateScope())
				{
					var outputProcessor = scope.ServiceProvider.GetRequiredService<IOutputProcessor>();
					await omniMudGame.Tick(outputProcessor);

					await Task.Delay(1);
				}				
			}
		}
	}
}
