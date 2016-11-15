using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Consoles;

namespace ConsoleScreenGameHelper.Core.Console.Panel
{
	public class StatusPanel : TextSurface
	{
		public StatusPanel(KeyValuePair<string, int> nameValue, int width, int height) : base(width, height)
		{
            SurfaceEditor editor = new SurfaceEditor(this);
            editor.Print(0, 0, nameValue.Key);
            editor.Print(7, 0, nameValue.Value.ToString());
		}

	}
}

