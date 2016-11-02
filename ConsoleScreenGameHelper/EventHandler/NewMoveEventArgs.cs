using System;
using ConsoleScreenGameHelper.Enum;


namespace ConsoleScreenGameHelper.EventHandler
{
	public class NewMoveEventArgs : EventArgs
	{
        public Direction Direction { get; set; }
		public NewMoveEventArgs(Direction direction)
		{
            Direction = direction;
		}
	}
}

