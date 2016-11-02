using System;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class FOV : Component
	{

        public override ComponentType ComponentType { get { return ComponentType.FOV; } }
        public bool Dirty { get; set; }

		public FOV ()
		{
		}

        public override void FireEvent(object sender, EventArgs e)
        {

        }

	}
}

