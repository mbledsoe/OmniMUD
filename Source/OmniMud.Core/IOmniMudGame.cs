using OmniMud.Core.Commands;

namespace OmniMud.Core
{
	public interface IOmniMudGame
	{
		void EnterGame(string connectionId);
		Task Tick(IOutputProcessor outputProcessor);
	}
}