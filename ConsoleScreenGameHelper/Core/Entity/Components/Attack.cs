using System;
using ConsoleScreenGameHelper.EventHandler;
using ConsoleScreenGameHelper.Core.Entity;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Attack : Component
	{

        public override ComponentType ComponentType { get { return ComponentType.Attack; } }
        public int BaseDamage { get; set; }
		public Attack ()
		{
            BaseDamage = 1;
		}

        public override void FireEvent(object sender, EventArgs e)
        {
            if(e.GetType() == typeof(NewAttackEventArgs))
            {
                (e as NewAttackEventArgs).Damage = BaseDamage;
                var self_a = GetComponent<Actor>(ComponentType.Actor);
                foreach(var en in self_a.Map.EntityContainer)
                {
                    var other_a = en.GetComponent<Actor>(ComponentType.Actor);
                    if(other_a != null)
                    {
                        if(other_a.Sprite.Position == (e as NewAttackEventArgs).Position)
                        {
                            en.FireEvent(this, new NewDamageEventArgs((e as NewAttackEventArgs).Damage));
                        }
                    }
                }
            }
        }
	}
}

