using OmniMud.Core.Commands.Info;
using OmniMud.Core.Commands.Misc;
using OmniMud.Core.Commands.Movement;
using OmniMud.Core.Services;
using OmniMud.Core.World;

namespace OmniMud.Core.Commands
{
	public class InputProcessor : IInputProcessor
	{
		private readonly IOutputQueue outputQueue;
		private readonly IWorldDataService worldDataService;
		private readonly ICharacterManager characterManager;
		private readonly IRoomService roomService;

		public InputProcessor(IOutputQueue outputQueue,
			IWorldDataService worldDataService,
			ICharacterManager characterManager,
			IRoomService roomService)
		{
			this.outputQueue = outputQueue;
			this.worldDataService = worldDataService;
			this.characterManager = characterManager;
			this.roomService = roomService;
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
					return new LookCommand(roomService);
				case "north":
					return new MoveCommand(Direction.North, worldDataService, characterManager, roomService, outputQueue);
				case "east":
					return new MoveCommand(Direction.East, worldDataService, characterManager, roomService, outputQueue);
				case "south":
					return new MoveCommand(Direction.South, worldDataService, characterManager, roomService, outputQueue);
				case "west":
					return new MoveCommand(Direction.West, worldDataService, characterManager, roomService, outputQueue);
				case "up":
					return new MoveCommand(Direction.Up, worldDataService, characterManager, roomService, outputQueue);
				case "down":
					return new MoveCommand(Direction.Down, worldDataService, characterManager, roomService, outputQueue);
				default:
					return new UnknownCommand(outputQueue);
			}
		}
	}
}
