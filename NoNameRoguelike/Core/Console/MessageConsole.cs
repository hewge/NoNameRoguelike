using System;
using System.Collections;
using System.Collections.Generic;
using ConsoleScreenGameHelper.Core.Console;
using ConsoleScreenGameHelper.EventHandler;
using SadConsole;
using Microsoft.Xna.Framework;

namespace NoNameRoguelike.Core.Console
{
	public class MessageConsole : ScrollingConsole
	{
        private readonly Queue _lines;

		public MessageConsole(int width, int height) : base( width, height, height + 75)
		{

            _lines = new Queue();
            //VirtualCursor.Position = new Point(1, 1);
            NoNameRoguelike.Core.Systems.MessageLog.FireNewMessage += Message;
		}

        private void Message(object sender, NewMessageEventArgs e)
        {
            //FIXME: Do this for now, cannot work out why it doesnot work as i want.
            _lines.Enqueue(e.Message);
            if(_lines.Count > 7)
            {
                _lines.Dequeue();
            }
            PrintMessage(e.Message);
        }

        public void PrintMessage(string text)
        {
            ShiftUp(1);
            VirtualCursor.Print(text).CarriageReturn();

        }

        public void PrintMessage(ColoredString text)
        {
            ShiftUp(1);
            VirtualCursor.Print(text).CarriageReturn();
        }
        public override void Render()
        {
            base.Render();
        }

	}
}

