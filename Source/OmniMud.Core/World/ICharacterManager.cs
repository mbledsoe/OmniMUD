namespace OmniMud.Core.World
{
	public interface ICharacterManager
	{
		void AddCharacter(string connectionId, int roomId);
		int GetCharacterRoomId(string connectionId);
		void MoveCharacter(string connectionId, int roomId);
	}
}