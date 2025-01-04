using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniMud.Core.Commands
{
	public class OutputQueue : IOutputQueue
	{
		private ConcurrentQueue<PlayerOutput> queue = new ConcurrentQueue<PlayerOutput>();

		public void Enqueue(PlayerOutput output)
		{
			queue.Enqueue(output);
		}

		public IList<PlayerOutput> DequeueOutput()
		{
			var queueCount = queue.Count;
			var dequeued = new List<PlayerOutput>();

			for (var i = 0; i < queueCount; i++)
			{
				queue.TryDequeue(out PlayerOutput? output);

				if (output != null)
				{
					dequeued.Add(output);
				}
			}

			return dequeued;
		}
	}
}
