using OmniMud.Core.Services;
using OmniMud.Core.World;

namespace OmniMud.Core.Commands.Movement
{
	public class MoveCommand : ICommand
	{
		private readonly Direction direction;
		private readonly IWorldDataService worldDataService;
		private readonly ICharacterManager characterManager;
		private readonly IRoomService roomService;
		private readonly IOutputQueue outputQueue;

		public MoveCommand(Direction direction,
			IWorldDataService worldDataService,			
			ICharacterManager characterManager,
			IRoomService roomService,
			IOutputQueue outputQueue)
		{
			this.direction = direction;
			this.worldDataService = worldDataService;
			this.characterManager = characterManager;
			this.roomService = roomService;
			this.outputQueue = outputQueue;
		}

		public void Execute(CommandContext context)
		{
			var room = worldDataService.GetRoomData(context.RoomId);
			var exit = room.Exits.FirstOrDefault(e => e.Direction == direction);

			if (exit == null)
			{
				var output = new PlayerOutput 
				{ 
					ConnectionId = context.ConnectionId, 
					Message = "Sorry, there is no exit in that direction." 
				};

				outputQueue.Enqueue(output);

				return;
			}

			characterManager.MoveCharacter(context.ConnectionId, exit.RoomId);
			roomService.Look(context.ConnectionId, exit.RoomId);
		}
	}
}
