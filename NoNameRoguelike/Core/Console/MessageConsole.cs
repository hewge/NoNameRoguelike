using System;
using System.Collections;
using System.Collections.Generic;
using ConsoleScreenGameHelper.Core.Console;
using ConsoleScreenGameHelper.EventHandler;
using SadConsole;
using Microsoft.Xna.Framework;

namespace NoNameRoguelike.Core.Console
{
	public class MessageConsole : BorderedConsole
	{
        private readonly Queue _lines;

		public MessageConsole(int width, int height) : base("Messages", width, height)
		{

            _lines = new Queue();
            VirtualCursor.Position = new Point(1, 1);
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
            PrintMessage();
        }

        public void PrintMessage()
        {
            /*
            ShiftDown(1);
            VirtualCursor.Print(text).CarriageReturn();
            VirtualCursor.Right(1);
            */
            //FIXME: This, works for now, later find out why the other method wasnt working, and maybe add a scrollbar & checkbox/button for debug messages?, and maybe a button for 'FullScreen View'.
            this.Clear();
            var cnt = 1;
            foreach(var l in _lines)
            {
                System.Console.WriteLine((string)l);
                this.Print(1, cnt, (string)l);
                this.VirtualCursor.CarriageReturn();
                cnt++;
            }

        }

        public void PrintMessage(ColoredString text)
        {
            ShiftDown(1);
            VirtualCursor.Print(text).CarriageReturn();
            VirtualCursor.Right(1);
        }
        public override void Render()
        {
            base.Render();
        }

	}
}

