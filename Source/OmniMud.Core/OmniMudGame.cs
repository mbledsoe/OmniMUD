using OmniMud.Core.Commands;
using OmniMud.Core.World;

namespace OmniMud.Core
{
	public class OmniMudGame : IOmniMudGame
	{
		private readonly IWorldDataService worldDataService;
		private readonly ICharacterManager characterManager;
		private readonly IInputQueue inputQueue;
		private readonly IInputProcessor inputProcessor;
		private readonly IOutputQueue outputQueue;

		public OmniMudGame(IWorldDataService worldDataService,
			ICharacterManager characterManager,
			IInputQueue inputQueue,
			IInputProcessor inputProcessor,
			IOutputQueue outputQueue)
		{
			this.worldDataService = worldDataService;
			this.characterManager = characterManager;
			this.inputQueue = inputQueue;
			this.inputProcessor = inputProcessor;
			this.outputQueue = outputQueue;
		}

		public async Task Tick(IOutputProcessor outputProcessor)
		{
			ProcessInput();
			await ProcessOutput(outputProcessor);
		}

		private async Task ProcessOutput(IOutputProcessor outputProcessor)
		{
			var dequeuedOutput = outputQueue.DequeueOutput();

			foreach (var output in dequeuedOutput)
			{
				await outputProcessor.ProcessOutput(output);
			}
		}

		private void ProcessInput()
		{
			var dequeuedInput = inputQueue.DequeueInput();

			foreach (var input in dequeuedInput)
			{
				inputProcessor.ProcessInput(input);
			}
		}

		public void EnterGame(string connectionId)
		{
			characterManager.AddCharacter(connectionId, worldDataService.StartRoomId);

			inputQueue.Enqueue(new PlayerInput
			{
				ConnectionId = connectionId,
				RawCommand = "look"
			});
		}
	}
}
