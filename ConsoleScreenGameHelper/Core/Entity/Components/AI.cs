using System;
using System.Collections.Generic;
using ConsoleScreenGameHelper.Interface;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class AI : Component
	{
        public int? TurnsAlerted { get; set; }

        public override ComponentType ComponentType { get{ return ComponentType.AI; } }

        List<IBehaviour> behaviours;

		public AI ()
		{
            behaviours = new List<IBehaviour>();
            behaviours.Add(new MoveAndAttack(GetParent()));
		}

        public override void FireEvent(object sender, EventArgs e)
        {

        }
	}
}

