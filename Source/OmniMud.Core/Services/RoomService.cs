using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmniMud.Core.Commands;
using OmniMud.Core.World;

namespace OmniMud.Core.Services
{
	public class RoomService : IRoomService
	{
		private readonly IOutputQueue outputQueue;
		private readonly IWorldDataService worldDataService;

		public RoomService(IOutputQueue outputQueue,
			IWorldDataService worldDataService)
		{
			this.outputQueue = outputQueue;
			this.worldDataService = worldDataService;
		}

		public void Look(string connectionId, int roomId)
		{
			var room = worldDataService.GetRoomData(roomId);

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
				ConnectionId = connectionId,
				Message = sb.ToString()
			};

			outputQueue.Enqueue(output);
		}
	}
}
