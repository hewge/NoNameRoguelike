using System;
using ConsoleScreenGameHelper.Enum;

namespace ConsoleScreenGameHelper.EventHandler
{
	public class NewInputEventArgs : EventArgs
	{
        public Input Input { get; set; }
		public NewInputEventArgs(Input input)
		{
            Input = input;
		}
	}
}

