using System;
using ConsoleScreenGameHelper.Enum;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Item : Component
	{
        public override ComponentType ComponentType { get { return ComponentType.Item; } }
        public ItemType ItemType { get; set; }
        public string Description { get; set; }

		public Item(ItemType itemType, string description = "An item of mundane means.")
		{
            ItemType = itemType;
            Description = description;
		}

        public override void FireEvent(object sender, EventArgs e)
        {

        }

	}
}

