using System;
using ConsoleScreenGameHelper.Interface;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ConsoleScreenGameHelper.EventHandler;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Map;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Actor : Component, IScheduleable
	{
        public int Time { get{ return Stats.Speed; } }
        [JsonIgnore]
        public Statistic Stats { get; set; }
        [JsonIgnore]
        public SpriteAnimation Sprite { get; set; }
        public MapLevel Map { get; set; }
        public override ComponentType ComponentType { get { return ComponentType.Actor; } }

        //These are just here to get scope outside of the constructor to be able to construct Stats and Sprite components in the Initialize.
        Color foreground;
        Color background;
        int strenght;
        int speed;
        int intelligence;
        int symbol;

		public Actor() : this(Color.Blue, Color.Black, null, 8, 8, 8,  '%')
		{
			
		}

        public Actor(MapLevel map) : this(Color.Blue, Color.Black, map, 8, 8, 8, '%')
        {

        }

		public Actor(Color foreground, Color background, MapLevel map, int str, int spd, int inte, int symbol = '%')
		{
            //KAN EJ LÄGGA TILL KOMPONENTER I EN ANNAN KOMPONENTS CONSTRUCTOR.
            this.Map = map;
            this.foreground = foreground;
            this.background = background;
            this.intelligence = inte;
            this.strenght = str;
            this.speed = spd;
            this.symbol = symbol;
		}

        public override void OnAfterInitialize()
        {
            this.GetParent().AddComponent(new Statistic(strenght, speed, intelligence));
            this.GetParent().AddComponent(new SpriteAnimation(symbol, foreground, background));
            this.GetParent().AddComponent(new Attack());
            this.GetParent().AddComponent(new Defence());
            Sprite = this.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation);
            Stats = this.GetComponent<Statistic>(ComponentType.Stats);

        }

        public override void FireEvent(object sender, EventArgs e)
        {
        }
        public void Die()
        {
            if(GetParent().logger != null)
            {
                GetParent().logger.Debug(string.Format("{0}, Has Died.", GetParent().NAME));
            }
            Map.MapData.Map.SetCellProperties(Sprite.Position.X, Sprite.Position.Y, true, true);
            Map.EntityContainer.Remove(GetParent());
        }
	}
}

