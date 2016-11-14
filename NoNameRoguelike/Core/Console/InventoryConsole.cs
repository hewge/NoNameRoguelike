using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ConsoleScreenGameHelper.Enum;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Entity.Components;
using SadConsole;
using SadConsole.Input;
using SadConsole.Consoles;

namespace NoNameRoguelike.Core.Console
{
	public class InventoryConsole : Window
	{
        public Inventory inventory { get{ return inventoryUser.GetComponent<Inventory>(ComponentType.Inventory); } }
        public BaseEntity inventoryUser { get; set;}
        private bool DropMode = false;
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
            if(this.IsVisible)
            {
                if(info.KeysPressed.Contains(AsciiKey.Get(Keys.J)))
                {
                    DropMode = DropMode == true ? false : true;
                }
                foreach(var k in inventory.inventory)
                {
                    if(info.KeysPressed.Contains(AsciiKey.Get((Keys)((int)char.ToUpper(k.Key)))))
                    {
                        if(DropMode)
                        {
                            SadConsole.Consoles.Window.Prompt(GetItemMessage(k), "Drop", "Cancel", (r) => { drop_prompt(r, k.Value); });
                        }
                        else
                        {
                            switch(k.Value.GetComponent<Item>(ComponentType.Item).ItemType)
                            {
                                case ItemType.Food:
                                    SadConsole.Consoles.Window.Prompt(GetItemMessage(k), "Eat", "Cancel", (r) => { food_prompt(r, k.Value); });
                                    break;
                                case ItemType.Equipment:
                                    SadConsole.Consoles.Window.Prompt(GetItemMessage(k), "Equip/UnEquip", "Cancel", (r) => { food_prompt(r, k.Value); });
                                    break;
                                case ItemType.Potion:
                                    SadConsole.Consoles.Window.Prompt(GetItemMessage(k), "Drink", "Cancel", (r) => { potion_prompt(r, k.Value); });
                                    break;
                                default:
                                    break;

                            }

                        }
                    }
                }
            }
            return false;

        }
        private void food_prompt(bool result, BaseEntity be)
        {
            if(result)
            {
                // DO IT

            }
            else
            {
                // Dont Do IT

            }

        }

        private void equipment_prompt(bool result, BaseEntity be)
        {
            if(result)
            {
                // DO IT

            }
            else
            {
                // Dont Do IT

            }
        }

        private void potion_prompt(bool result, BaseEntity be)
        {
            if(result)
            {
                // DO IT

            }
            else
            {
                // Dont Do IT

            }

        }

        private void drop_prompt(bool result, BaseEntity be)
        {
            if(result)
            {
                System.Console.WriteLine("Drop Item {0}", be.NAME);
                inventory.DropItem(be);
            }
            else
            {
                System.Console.WriteLine("Do Not Drop Item {0}", be.NAME);
            }
        }

        private ColoredString GetItemMessage(KeyValuePair<char, BaseEntity> de)
        {
            ColoredString str = new ColoredString(string.Format("{0}", de.Value.NAME));
            str += ":".CreateColored(Color.White);
            string s = char.ConvertFromUtf32(de.Value.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Symbol).ToString();
            str += string.Format("{0}", s).CreateColored(
                    de.Value.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Foreground,
                    de.Value.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Background);
            str += ": A ".CreateColored(Color.White);
            str += string.Format("{0}, ", de.Value.GetComponent<Item>(ComponentType.Item).ItemType).CreateColored(Color.White);
            str += string.Format("{0}", de.Value.GetComponent<Item>(ComponentType.Item).Description).CreateColored(Color.White);
            return str;
        }

        public override void Render()
        {
            if(DropMode)
            {
                this.Title = "Inventory - DropMode";
            }
            else
            {
                this.Title = "Inventory";
            }
            if(inventory != null)
            {
                this.VirtualCursor.Position = new Point(0, 1);
                foreach(var item in inventory.inventory)
                {
                    this.VirtualCursor.Down(1);
                    this.VirtualCursor.Right(1);
                    var s = char.ConvertFromUtf32(item.Value.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Symbol);
                    ColoredString str = new ColoredString("");
                    str += string.Format("{0} : ", item.Key).CreateColored(Color.White);
                    str += new ColoredString(s, item.Value.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Foreground, item.Value.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Background);
                    str += string.Format(" : {0}", item.Value.NAME).CreateColored(Color.White);

                    this.VirtualCursor.Print(str).NewLine();
                }
            }
            base.Render();
        }
	}
}

