using System;
using SadConsole;

namespace ConsoleScreenGameHelper.Interface
{
	public interface ILog
	{
        void Write(ColoredString message);
        void Write(string message);
        void Debug(string message);
	}
}

