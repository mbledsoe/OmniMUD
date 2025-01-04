using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniMud.Core.World
{
	public class WorldDataService : IWorldDataService
	{
		private readonly IDictionary<int, RoomData> rooms;

		public WorldDataService(int startRoomId, IDictionary<int, RoomData> rooms)
		{
			StartRoomId = startRoomId;
			this.rooms = rooms;
		}

		public int StartRoomId { get; private set; }

		public RoomData GetRoomData(int roomId)
		{
			if (!rooms.ContainsKey(roomId))
			{
				throw new Exception($"Room {roomId} does not exist.");
			}

			return rooms[roomId];
		}
	}
}
