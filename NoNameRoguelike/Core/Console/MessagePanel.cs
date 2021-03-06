﻿using ConsoleScreenGameHelper.Core.Console;
using ConsoleScreenGameHelper.EventHandler;
using SadConsole;

namespace NoNameRoguelike.Core.Console
{
	public class MessagePanel : BorderedScrollingConsole
	{

		public MessagePanel(int width, int height) : base("Messages", width, height, height + 350)
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
            VirtualCursor.Print(text).NewLine();
        }
        public override void Render()
        {
            base.Render();
        }

	}
}

