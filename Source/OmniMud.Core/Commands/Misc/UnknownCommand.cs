namespace OmniMud.Core.Commands.Misc
{
	public class UnknownCommand : ICommand
	{
		private readonly IOutputQueue outputQueue;

		public UnknownCommand(IOutputQueue outputQueue)
		{
			this.outputQueue = outputQueue;
		}

		public void Execute(CommandContext context)
		{
			var output = new PlayerOutput
			{
				ConnectionId = context.ConnectionId,
				Message = "Sorry, I don't understand that command."
			};

			outputQueue.Enqueue(output);
		}
	}
}
