using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ConsoleScreenGameHelper.EventHandler;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Map;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Actor : Component
	{
        [JsonIgnore]
        public Statistic Stats { get; set; }
        [JsonIgnore]
        public SpriteAnimation Sprite { get; set; }
        public MapLevel Map { get; set; }
        public override ComponentType ComponentType { get { return ComponentType.Actor; } }

        //These are just here to get scope outside of the constructor to be able to construct Stats and Sprite components in the Initialize.
        Color foreground;
        Color background;
        int health;
        int maxHealth;
        int energy;
        int maxEnergy;
        int symbol;

		public Actor() : this(Color.Blue, Color.Black, null, 100, 100, 100, 100, '%')
		{
			
		}

        public Actor(MapLevel map) : this(Color.Blue, Color.Black, map, 100, 100, 100, 100, '%')
        {

        }

		public Actor(Color foreground, Color background, MapLevel map, int health = 100, int maxHealth = 100, int energy = 100, int maxEnergy = 100, int symbol = '%')
		{
            //KAN EJ LÄGGA TILL KOMPONENTER I EN ANNAN KOMPONENTS CONSTRUCTOR.
            this.Map = map;
            this.foreground = foreground;
            this.background = background;
            this.health = health;
            this.maxHealth = maxHealth;
            this.energy = energy;
            this.maxEnergy = maxEnergy;
            this.symbol = symbol;
		}

        public override void OnAfterInitialize()
        {
            this.GetParent().AddComponent(new Statistic(health, maxHealth, energy, maxEnergy));
            this.GetParent().AddComponent(new SpriteAnimation(symbol, foreground, background));
            Stats = this.GetComponent<Statistic>(ComponentType.Stats);
            Sprite = this.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation);

        }

        public override void FireEvent(object sender, EventArgs e)
        {
            if(e.GetType() == typeof(NewDamageEventArgs))
            {
                //Take Damage , by first finding out if any "components" want to modify it first.
                TakeDamage((e as NewDamageEventArgs).Damage);

            }
        }

        private void TakeDamage(int damage)
        {
            Stats.Health -= damage;
            if(Stats.Health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GetParent().Die();
        }
	}
}

