﻿using System;

namespace ConsoleScreenGameHelper.EventHandler
{
	public class NewMessageEventArgs : EventArgs
	{
        public string Message { get; set; }
		public NewMessageEventArgs(string message)
		{
            this.Message = message;
		}
	}
}
