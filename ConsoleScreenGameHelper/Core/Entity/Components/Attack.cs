using System;
using Microsoft.Xna.Framework;
using SadConsole;
using RogueSharp.DiceNotation;
using ConsoleScreenGameHelper.EventHandler;
using ConsoleScreenGameHelper.Core.Entity;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Attack : Component
	{

        public override ComponentType ComponentType { get { return ComponentType.Attack; } }
        public Statistic Stats { get{ return this.GetComponent<Actor>(ComponentType.Actor).Stats; }  }
        public int AttackValue { get{ return Stats.Attack; } }
		public Attack ()
		{
		}

        private float ResolveAttack()
        {
            if(GetComponent<Statistic>(ComponentType.Stats).Energy < 2)
            {
                return 0f;
            }
            GetComponent<Statistic>(ComponentType.Stats).Energy -= 2;
            float hitPercent = 0;
            DiceExpression attackDice = new DiceExpression().Dice(AttackValue, 100);
            DiceResult attackResult = attackDice.Roll();

            foreach( TermResult termResult in attackResult.Results )
            {
                if(termResult.Value >= (100 - Stats.AttackChance ))
                {
                    hitPercent++;
                }
            }

            float hp = hitPercent/(float)AttackValue;
            System.Console.WriteLine("HitPercent:{0}", hp);

            return hp;
        }

        public override void FireEvent(object sender, EventArgs e)
        {
            if(e.GetType() == typeof(NewAttackEventArgs))
            {
                var hitPercent = ResolveAttack();
                var dmg = AttackValue * hitPercent;

                System.Console.WriteLine("Damage:{0}", (int)dmg);
                (e as NewAttackEventArgs).Damage = (int)dmg;

                //TODO: Modify Damage Here ?
                var self_a = GetComponent<Actor>(ComponentType.Actor);
                BaseEntity other = self_a.Map.GetEntityAt((e as NewAttackEventArgs).Position.X, (e as NewAttackEventArgs).Position.Y);
                if(other != null)
                {
                    if(GetParent().logger != null)
                    {
                        ColoredString str = new ColoredString(string.Format("{0}, ", GetParent().NAME));
                        str+="Attacks".CreateColored(Color.Red);
                        str+=string.Format(" {0} for", other.NAME).CreateColored(Color.White);
                        str+=string.Format(" {0} ", (int)dmg).CreateColored(Color.DarkRed);
                        str+="damage".CreateColored(Color.Red);
                        str+=".".CreateColored(Color.White);

                        GetParent().logger.Write(str);
                    }

                    other.FireEvent(this, new NewDamageEventArgs((e as NewAttackEventArgs).Damage));
                }
            }
        }
	}
}

