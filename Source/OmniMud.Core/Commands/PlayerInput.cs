namespace OmniMud.Core.Commands
{
	public class PlayerInput
	{
		// TODO: Change to PlayerId and map connections to players
		public required string ConnectionId { get; set; }
		public required string RawCommand { get; set; }
	}
}
