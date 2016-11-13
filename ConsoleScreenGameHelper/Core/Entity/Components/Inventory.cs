using System;
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

        public override void FireEvent(object sender, EventArgs e)
        {

        }
	}
}

