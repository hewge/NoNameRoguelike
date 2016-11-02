using System;
using System.Collections;
using ConsoleScreenGameHelper.EventHandler;
using ConsoleScreenGameHelper.Interface;

namespace NoNameRoguelike.Core.Systems
{
	public class MessageLog : ILog
	{
        private readonly Queue _lines;
        public bool DebugMessages { get; set; }
        private static event EventHandler<NewMessageEventArgs> _fireNewMessage;
        public static event EventHandler<NewMessageEventArgs> FireNewMessage
        {
            add { _fireNewMessage += value; }
            remove { _fireNewMessage -= value; }
        }
        public MessageLog(bool debugMessages = false)
        {
            _lines = new Queue();
            DebugMessages = debugMessages;
        }

        public void Write(string message)
        {
            _lines.Enqueue(message);
            if(_fireNewMessage != null)
            {
                _fireNewMessage(this, new NewMessageEventArgs(message));
            }
        }
        public void Debug(string message)
        {
            if(DebugMessages)
            {
                this.Write("(DEBUG)"+message);
            }
        }
	}
}
