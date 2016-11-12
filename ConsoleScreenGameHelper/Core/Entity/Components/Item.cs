using System;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Item : Component
	{
        public override ComponentType ComponentType { get { return ComponentType.Item; } }

		public Item()
		{
		}

        public override void FireEvent(object sender, EventArgs e)
        {

        }

	}
}

