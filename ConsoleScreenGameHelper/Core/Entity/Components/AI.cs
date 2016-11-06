using System;
using ConsoleScreenGameHelper.Core.Entity.AI.Behaviour;
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
		}

        public override void OnAfterInitialize()
        {
            behaviours.Add(new MoveAndAttack(GetParent()));
        }

        public void Act()
        {
            behaviours[0].Act();
        }

        public override void FireEvent(object sender, EventArgs e)
        {

        }
	}
}

