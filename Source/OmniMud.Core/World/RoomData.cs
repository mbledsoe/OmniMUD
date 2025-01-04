using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniMud.Core.World
{
	public class RoomData
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public required string Description { get; set; }
		public List<Exit> Exits { get; set; } = new();
	}
}
