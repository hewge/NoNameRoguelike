using System;
using Microsoft.Xna.Framework;
using ConsoleScreenGameHelper.Core.Entity.Components;
using SadConsole;
using SadConsole.Consoles;

namespace NoNameRoguelike.Core.Console
{
	public class InventoryConsole : Window
	{
        public Inventory inventory { get; set; }
		public InventoryConsole(int width, int height) : base(width, height)
		{
            Title = "Inventory";
            CloseOnESC = true;
            Dragable = true;
            ProcessMouseWithoutFocus = true;
            CanFocus = false;
            MouseCanFocus = false;
		}

        public override void Render()
        {
            this.VirtualCursor.Position = new Point(1, 1);
            if(inventory != null)
            {
                foreach(var item in inventory.inventory)
                {
                    this.VirtualCursor.Print(string.Format("{0} : {1}", item.Key, item.Value.NAME)).NewLine();
                }
            }
            base.Render();
        }
	}
}

