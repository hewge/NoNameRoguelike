﻿using System;
using RogueSharp.DiceNotation;
using ConsoleScreenGameHelper.EventHandler;
using ConsoleScreenGameHelper.Core.Entity;


namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Defence : Component
	{

        public override ComponentType ComponentType { get { return ComponentType.Defence; } }
        public Statistic Stats { get{ return this.GetComponent<Actor>(ComponentType.Actor).Stats; }  }

		public Defence ()
		{
		}

        private float ResolveDefence()
        {

            if(GetComponent<Statistic>(ComponentType.Stats).Energy < 3)
            {
                return 0f;
            }

            GetComponent<Statistic>(ComponentType.Stats).Energy -= 3;
            float blockPercent = 0;
            DiceExpression blockDice = new DiceExpression().Dice(Stats.Defence, 100);
            DiceResult blockResult = blockDice.Roll();

            foreach( TermResult termResult in blockResult.Results )
            {
                if(termResult.Value >= (100 - Stats.DefenceChance))
                {
                    blockPercent++;
                }
            }

            var bp = blockPercent/(float)Stats.Defence;
            if(bp == 1 && Stats.Defence >= 20)
            {
                return 1;
            }

            bp = bp*(float)0.80;

            System.Console.WriteLine("BlockPercent:{0}", bp);

            return bp;

        }

        public override void FireEvent(object sender, EventArgs e)
        {
            if(e.GetType() == typeof(NewDamageEventArgs))
            {
                var blockPercent = ResolveDefence();
                var dmg = (e as NewDamageEventArgs).Damage - ((e as NewDamageEventArgs).Damage * blockPercent);
                System.Console.WriteLine("Damage Inflicted:{0}", (int)dmg);
                //Take Damage , by first finding out if any "components" want to modify it first.
                if(GetParent().logger != null)
                {
                    GetParent().logger.Write(string.Format("{0}, Blocks {1}% of incoming damage, so {2} damage is Inflicted.", GetParent().NAME, blockPercent, (int)dmg));
                }
                Stats.Health -= (int)dmg;
            }

        }
	}
}

