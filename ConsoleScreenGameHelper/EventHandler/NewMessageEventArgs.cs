using System;
using SadConsole;

namespace ConsoleScreenGameHelper.EventHandler
{
	public class NewMessageEventArgs : EventArgs
	{
        public ColoredString Message { get; set; }
		public NewMessageEventArgs(ColoredString message)
		{
            this.Message = message;
		}
	}
}
