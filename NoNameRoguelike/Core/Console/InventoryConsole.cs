using System;
using SadConsole;
using SadConsole.Consoles;

namespace NoNameRoguelike.Core.Console
{
	public class InventoryConsole : Window
	{
		public InventoryConsole(int width, int height) : base(width, height)
		{
            Title = "Inventory";
            CloseOnESC = true;
            Dragable = true;
            ProcessMouseWithoutFocus = true;
            CanFocus = false;
		}
	}
}

