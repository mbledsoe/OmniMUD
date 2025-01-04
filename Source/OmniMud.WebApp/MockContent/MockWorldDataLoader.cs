using System.Text.Json.Serialization;
using Newtonsoft.Json;
using OmniMud.Core.World;

namespace OmniMud.WebApp.MockContent
{
	public class MockWorldDataLoader
	{
		public IDictionary<int, RoomData> LoadWorldData()
		{
			var mockWorldDataJson = File.ReadAllText("MockContent\\MockWorldData.json");
			var mockWorldData = JsonConvert.DeserializeObject<MockWorldData>(mockWorldDataJson);

			var rooms = new Dictionary<int, RoomData>();

			foreach (var room in mockWorldData.Rooms)
			{
				rooms.Add(room.Id, room);
			}

			return rooms;
		}
	}
}
