using System.Text;
using OmniMud.Core.Services;
using OmniMud.Core.World;

namespace OmniMud.Core.Commands.Info
{
	public class LookCommand : ICommand
	{
		private readonly IRoomService roomService;

		public LookCommand(IRoomService roomService)
		{
			this.roomService = roomService;
		}

		public void Execute(CommandContext context)
		{
			roomService.Look(context.ConnectionId, context.RoomId);
		}
	}
}
