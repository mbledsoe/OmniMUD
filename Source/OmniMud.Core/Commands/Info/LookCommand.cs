using System.Text;
using OmniMud.Core.World;

namespace OmniMud.Core.Commands.Info
{
	public class LookCommand : ICommand
	{
		private readonly IOutputQueue outputQueue;
		private readonly IWorldDataService worldDataService;

		public LookCommand(IOutputQueue outputQueue, 
			IWorldDataService worldDataService)
		{
			this.outputQueue = outputQueue;
			this.worldDataService = worldDataService;
		}

		public void Execute(CommandContext context)
		{
			var room = worldDataService.GetRoomData(context.RoomId);

			var sb = new StringBuilder();
			sb.Append($"<div class=\"room-title\">{room.Title}</div>");
			sb.Append($"<div class=\"room-desc\">{room.Description}</div>");
			sb.Append("<div class=\"room-exits\">");

			foreach (var exit in room.Exits)
			{
				var exitRoom = worldDataService.GetRoomData(exit.RoomId);
				sb.Append($"<div class=\"room-exit\">[{exit.Direction.ToString()}] {exitRoom.Title}</div>");
			}

			sb.Append("</div>");

			var output = new PlayerOutput
			{
				ConnectionId = context.ConnectionId,
				Message = sb.ToString()
			};

			outputQueue.Enqueue(output);
		}
	}
}
