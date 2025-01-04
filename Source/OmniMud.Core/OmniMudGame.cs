using OmniMud.Core.Commands;
using OmniMud.Core.Services;
using OmniMud.Core.World;

namespace OmniMud.Core
{
	public class OmniMudGame : IOmniMudGame
	{
		private readonly ICharacterManager characterManager;
		private readonly IWorldDataService worldDataService;
		private readonly IRoomService roomService;
		private readonly IInputQueue inputQueue;
		private readonly IInputProcessor inputProcessor;
		private readonly IOutputQueue outputQueue;

		public OmniMudGame(IInputQueue inputQueue,
			IInputProcessor inputProcessor,
			IOutputQueue outputQueue,			
			ICharacterManager characterManager,
			IWorldDataService worldDataService,
			IRoomService roomService)
		{
			this.inputQueue = inputQueue;
			this.inputProcessor = inputProcessor;
			this.outputQueue = outputQueue;
			this.characterManager = characterManager;
			this.worldDataService = worldDataService;
			this.roomService = roomService;
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
			roomService.Look(connectionId, worldDataService.StartRoomId);			
		}
	}
}
