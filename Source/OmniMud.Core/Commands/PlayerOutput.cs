namespace OmniMud.Core.Commands
{
	public class PlayerOutput
	{
		// TODO: change to PlayerId and map to ConnectionId
		public required string ConnectionId { get; set; }
		public required string Message { get; set; }
	}
}