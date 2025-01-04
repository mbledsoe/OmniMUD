using System.Collections.Concurrent;

namespace OmniMud.Core.Commands
{
	public class InputQueue : IInputQueue
	{
		private ConcurrentQueue<PlayerInput> queue = new ConcurrentQueue<PlayerInput>();

		public void Enqueue(PlayerInput input)
		{
			queue.Enqueue(input);
		}

		public IList<PlayerInput> DequeueInput()
		{
			var queueCount = queue.Count;
			var dequeued = new List<PlayerInput>();

			for (var i = 0; i < queueCount; i++)
			{
				queue.TryDequeue(out PlayerInput? input);

				if (input != null)
				{
					dequeued.Add(input);
				}
			}

			return dequeued;
		}
	}
}
