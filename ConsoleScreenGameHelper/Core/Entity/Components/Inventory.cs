using System;
using SadConsole;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Inventory : Component
	{
        public override ComponentType ComponentType { get { return ComponentType.Inventory; } }

        public Dictionary<char, BaseEntity> inventory;

		public Inventory()
		{
            inventory = new Dictionary<char, BaseEntity>();
		}

        public void AddItem(BaseEntity be)
        {
            for(char c='A';c<='Z';c++)
            {
                if(!inventory.ContainsKey(c))
                {
                    inventory.Add(c, be);
                    return;
                }
            }
            throw new IndexOutOfRangeException("Inventory full.");
        }

        public void RemoveItem(BaseEntity be)
        {
            if(inventory.ContainsValue(be))
            {
                foreach(var i in inventory)
                {
                    if(i.Value == be)
                    {
                        inventory.Remove(i.Key);
                        return;
                    }
                }
            }
            throw new IndexOutOfRangeException("Item Not Found.");
        }

        public void RemoveItem(char k)
        {
            if(inventory.ContainsKey(k))
            {
                inventory.Remove(k);
            }
        }

        public void PickupItem()
        {
            //From Ground
            Point inventoryUserPosition = GetParent().GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position;
            var a = GetParent().GetComponent<Actor>(ComponentType.Actor);
            if(a != null)
            {
                var i = a.Map.GetItemAt(inventoryUserPosition.X, inventoryUserPosition.Y);
                if(i != null)
                {
                    AddItem(i);
                    i.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).IsVisible = false;
                    i.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position = new Point(0, 0);
                    if(GetParent().logger != null)
                    {
                        ColoredString str = new ColoredString("Item : ");
                        str += string.Format("{0}", i.NAME).CreateColored(Color.Cyan);
                        str += ", Picked up.".CreateColored(Color.White);
                        GetParent().logger.Write(str);
                    }

                }
                else
                {
                    if(GetParent().logger != null)
                    {
                        GetParent().logger.Write("There is no item to pick up.");
                    }
                }
            }
        }

        public void DropItem(BaseEntity be)
        {
            //To Ground
            RemoveItem(be);
            Point inventoryUserPosition = GetParent().GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position;
            be.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).IsVisible = true;
            be.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position = new Point(inventoryUserPosition.X, inventoryUserPosition.Y);
        }

        public override void FireEvent(object sender, EventArgs e)
        {

        }
	}
}

