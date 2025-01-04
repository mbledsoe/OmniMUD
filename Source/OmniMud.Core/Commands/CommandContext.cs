using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniMud.Core.Commands
{
	public class CommandContext
	{
		// TODO: Change this to PlayerId and map to ConnectionId
		public required string ConnectionId { get; set; }
		public int RoomId { get; internal set; }
	}
}
