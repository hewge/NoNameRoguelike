using System;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.Core.Entity.AI.Behaviour;
using System.Collections.Generic;
using ConsoleScreenGameHelper.Interface;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class AI : Component
	{
        public int? TurnsAlerted { get; set; }

        public override ComponentType ComponentType { get{ return ComponentType.AI; } }

        Dictionary<string, IBehaviour> behaviours;

		public AI ()
		{
            behaviours = new Dictionary<string, IBehaviour>();
		}

        public override void OnAfterInitialize()
        {
            behaviours.Add("MoveAndAttack", new MoveAndAttack(GetParent()));
            behaviours.Add("RunAway", new RunAway(GetParent()));
            behaviours.Add("Roam", new Roam(GetParent()));
        }

        public void Act()
        {
            Actor a = GetComponent<Actor>(ComponentType.Actor);

            if(a.Stats.Health < (a.Stats.MaxHealth*0.16) && behaviours.ContainsKey("RunAway"))
            {
                behaviours["RunAway"].Act();
            }
            else
            {
                behaviours["MoveAndAttack"].Act();
            }
        }

        public override void FireEvent(object sender, EventArgs e)
        {

        }
	}
}

