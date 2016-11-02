using System;

namespace ConsoleScreenGameHelper.Interface
{
	public interface ILog
	{
        void Write(string message);
        void Debug(string message);
	}
}

