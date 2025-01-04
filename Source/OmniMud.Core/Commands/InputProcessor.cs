using OmniMud.Core.Commands.Info;
using OmniMud.Core.Commands.Misc;
using OmniMud.Core.World;

namespace OmniMud.Core.Commands
{
	public class InputProcessor : IInputProcessor
	{
		private readonly IOutputQueue outputQueue;
		private readonly IWorldDataService worldDataService;
		private readonly ICharacterManager characterManager;

		public InputProcessor(IOutputQueue outputQueue,
			IWorldDataService worldDataService,
			ICharacterManager characterManager)
		{
			this.outputQueue = outputQueue;
			this.worldDataService = worldDataService;
			this.characterManager = characterManager;
		}

		public void ProcessInput(PlayerInput input)
		{
			var command = CreateCommand(input);
			var context = CreateCommandContext(input);

			command.Execute(context);
		}

		private CommandContext CreateCommandContext(PlayerInput input)
		{
			return new CommandContext
			{
				ConnectionId = input.ConnectionId,
				RoomId = characterManager.GetCharacterRoomId(input.ConnectionId)
			};
		}

		private ICommand CreateCommand(PlayerInput input)
		{
			var commandWord = input.RawCommand.ToLower().Split(' ').FirstOrDefault();

			switch (commandWord)
			{
				case "look":
					return new LookCommand(outputQueue, worldDataService);
				default:
					return new UnknownCommand(outputQueue);
			}
		}
	}
}
