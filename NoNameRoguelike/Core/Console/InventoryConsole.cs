using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Entity.Components;
using SadConsole;
using SadConsole.Input;
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

        public override bool ProcessKeyboard(KeyboardInfo info)
        {
            //TODO: Have this use Colored strings and make it so an item of type Equipment is equippable.(or unequippable)
            foreach(var k in inventory.inventory)
            {
                if(info.KeysPressed.Contains(AsciiKey.Get((Keys)((int)char.ToUpper(k.Key)))))
                {
                    SadConsole.Consoles.Window.Message(string.Format("{0}:{1}: A {2}, {3}", k.Value.NAME, char.ConvertFromUtf32(k.Value.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Symbol), k.Value.GetComponent<Item>(ComponentType.Item).ItemType, k.Value.GetComponent<Item>(ComponentType.Item).Description), "Ok", null);
                    return true;
                }
            }
            return false;

        }

        public override void Render()
        {
            if(inventory != null)
            {
                this.VirtualCursor.Position = new Point(0, 1);
                foreach(var item in inventory.inventory)
                {
                    this.VirtualCursor.Down(1);
                    this.VirtualCursor.Right(1);
                    this.VirtualCursor.Print(string.Format("{0} : {1}", item.Key, item.Value.NAME)).NewLine();
                }
            }
            base.Render();
        }
	}
}

