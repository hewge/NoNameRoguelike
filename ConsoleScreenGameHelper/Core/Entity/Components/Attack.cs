using System;
using RogueSharp.DiceNotation;
using ConsoleScreenGameHelper.EventHandler;
using ConsoleScreenGameHelper.Core.Entity;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Attack : Component
	{

        public override ComponentType ComponentType { get { return ComponentType.Attack; } }
        public Statistic Stats { get{ return this.GetComponent<Actor>(ComponentType.Actor).Stats; }  }
		public Attack ()
		{
		}

        private float ResolveAttack()
        {
            float hitPercent = 0;
            DiceExpression attackDice = new DiceExpression().Dice(Stats.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            foreach( TermResult termResult in attackResult.Results )
            {
                if(termResult.Value >= (100 - Stats.AttackChance ))
                {
                    hitPercent++;
                }
            }

            var hp = hitPercent/(float)Stats.Attack;

            System.Console.WriteLine("HitPercent:{0}", hp);

            return hp;
        }

        public override void FireEvent(object sender, EventArgs e)
        {
            if(e.GetType() == typeof(NewAttackEventArgs))
            {
                var hitPercent = ResolveAttack();
                var dmg = Stats.Attack * hitPercent;

                System.Console.WriteLine("Damage:{0}", (int)dmg);
                (e as NewAttackEventArgs).Damage = (int)dmg;

                //TODO: Modify Damage Here ?

                var self_a = GetComponent<Actor>(ComponentType.Actor);
                BaseEntity other = self_a.Map.GetEntityAt((e as NewAttackEventArgs).Position.X, (e as NewAttackEventArgs).Position.Y);
                if(other != null)
                {
                    other.FireEvent(this, new NewDamageEventArgs((e as NewAttackEventArgs).Damage));
                }
            }
        }
	}
}

