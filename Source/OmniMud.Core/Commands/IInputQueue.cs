
namespace OmniMud.Core.Commands
{
	public interface IInputQueue
	{
		IList<PlayerInput> DequeueInput();
		void Enqueue(PlayerInput input);
	}
}