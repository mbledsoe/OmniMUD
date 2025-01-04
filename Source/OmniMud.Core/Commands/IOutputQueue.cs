
namespace OmniMud.Core.Commands
{
	public interface IOutputQueue
	{
		IList<PlayerOutput> DequeueOutput();
		void Enqueue(PlayerOutput output);
	}
}