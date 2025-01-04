namespace OmniMud.Core.World
{
	public class CharacterManager : ICharacterManager
	{
		private Dictionary<string, CharacterData> characters = new Dictionary<string, CharacterData>();

		public void AddCharacter(string connectionId, int roomId)
		{
			var character = new CharacterData { ConnectionId = connectionId, RoomId = roomId };

			characters.Add(character.ConnectionId, character);
		}

		private CharacterData GetCharacter(string connectionId)
		{
			if (!characters.ContainsKey(connectionId))
			{
				throw new Exception($"Character {connectionId} does not exist.");
			}

			return characters[connectionId];
		}

		public int GetCharacterRoomId(string connectionId)
		{
			var character = GetCharacter(connectionId);

			return character.RoomId;
		}

		public void MoveCharacter(string connectionId, int roomId)
		{
			var character = GetCharacter(connectionId);

			character.RoomId = roomId;
		}
	}
}
