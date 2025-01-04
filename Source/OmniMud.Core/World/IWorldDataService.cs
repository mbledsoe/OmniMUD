namespace OmniMud.Core.World
{
	public interface IWorldDataService
	{
		int StartRoomId { get; }

		RoomData GetRoomData(int roomId);
	}
}