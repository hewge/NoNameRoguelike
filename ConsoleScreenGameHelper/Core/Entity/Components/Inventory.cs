using System;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Inventory : Component
	{
        public override ComponentType ComponentType { get { return ComponentType.Inventory; } }

		public Inventory()
		{
		}

        public override void FireEvent(object sender, EventArgs e)
        {

        }
	}
}

