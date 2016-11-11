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

		public MessageConsole(int width, int height) : base( width, height, height + 75)
		{
            NoNameRoguelike.Core.Systems.MessageLog.FireNewMessage += Message;
		}

        private void Message(object sender, NewMessageEventArgs e)
        {
            PrintMessage(e.Message);
        }

        public void PrintMessage(string text)
        {
            VirtualCursor.Print(text).NewLine();
        }

        public void PrintMessage(ColoredString text)
        {
            VirtualCursor.Print(text).CarriageReturn();
        }
        public override void Render()
        {
            base.Render();
        }

	}
}

