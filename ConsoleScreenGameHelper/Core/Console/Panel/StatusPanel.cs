using System;
using System.Collections.Generic;
using ConsoleScreenGameHelper.Core.DataContainer;
using ConsoleScreenGameHelper.Interface;
using SadConsole;
using SadConsole.Consoles;

namespace ConsoleScreenGameHelper.Core.Console.Panel
{
	public class StatusPanel : TextSurface, IStatusPanel
	{
        SurfaceEditor editor;
        public BaseStat stat;
		public StatusPanel(BaseStat bs, int width, int height) : base(width, height)
		{
            editor = new SurfaceEditor(this);
            stat = bs;
		}

        public void Update()
        {
            editor.Print(0, 0, stat.Name);
            editor.Print(14, 0, stat.Value.ToString());
            if(stat.AltValue != 0)
            {
                editor.Print(17, 0, stat.AltValue.ToString());
            }
        }

	}
}

