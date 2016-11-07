using System;
using System.Collections.Generic;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Console;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.EventHandler;
using Microsoft.Xna.Framework;
using MathHelper = Microsoft.Xna.Framework.MathHelper;
using SadConsole;

namespace NoNameRoguelike.Core.Console
{
    //TODO: Refactor this class,, should be able to do "Character status ", in a better way.
	public class CharacterStatusPanel : BorderedConsole
	{
        private string _characterName;
        private int _health;
        private int _maxHealth;
        private int _energy;
        private int _maxEnergy;

        private List<Actor> targetList;

        public string CharacterName
        {
            set{ _characterName = value; RedrawPanel(); }
        }

        public int Health
        {
            set{ _health = MathHelper.Clamp(value, 0, _maxHealth); RedrawPanel(); }
        }
        public int MaxHealth
        {
            set{ _maxHealth = value; RedrawPanel(); }
        }

        public int Energy
        {
            set{ _energy = MathHelper.Clamp(value, 0, _maxEnergy); RedrawPanel(); }
        }
        public int MaxEnergy
        {
            set{ _maxEnergy = value; RedrawPanel(); }
        }

		public CharacterStatusPanel(int width, int height) : base("Status", width, height)
		{
            //TODO: ReDo this whole class in a better way.
            targetList = new List<Actor>();
		}

        public void StatusChanged(object sender, EventArgs e)
        {
            if(e.GetType() == typeof(StatsChangedEventArgs))
            {
                Health = (e as StatsChangedEventArgs).Health;
                MaxHealth = (e as StatsChangedEventArgs).MaxHealth;
                Energy = (e as StatsChangedEventArgs).Energy;
                MaxEnergy = (e as StatsChangedEventArgs).MaxEnergy;
            }
        }

        public override void Update()
        {
            //FIXME: Insane code, fix this as soon as possible.
            List<Actor> rmLst = new List<Actor>();
            if(targetList.Count > 0)
            {
                foreach(var t in targetList)
                {
                    if(t.Stats.Health <= 0)
                    {
                        rmLst.Add(t);
                    }
                }
                if(rmLst.Count > 0)
                {
                    foreach(var r in rmLst)
                    {
                        targetList.Remove(r);
                    }
                    RedrawPanel();
                    rmLst = null;
                }
            }
        }

        public void actor_StatusChanged(object sender, EventArgs e)
        {
            //FIXME: Way to complex, try to refactor this as soon as possible.
            if(e.GetType() == typeof(StatsChangedEventArgs))
            {
                if((sender as Statistic).IsInFov == true)
                {
                    var a = (sender as Statistic).GetComponent<Actor>(ComponentType.Actor);
                    if((sender as Statistic).Health <= 0)
                    {
                        if(targetList.Contains(a))
                        {
                            targetList.Remove(a);
                            RedrawPanel();
                            return;
                        }
                    }
                    if(!targetList.Contains(a))
                    {
                        targetList.Add(a);
                        RedrawPanel();
                    }
                }
                else
                {
                    var a = (sender as Statistic).GetComponent<Actor>(ComponentType.Actor);
                    if(targetList.Contains(a))
                    {
                        targetList.Remove(a);
                        RedrawPanel();
                    }
                }
            }
            RedrawPanel();
        }

        private void DrawInfo(int x, int y, Actor a)
        {
            // Targets
            var healthStatus = a.Stats.Health.ToString().CreateColored(Color.LightGreen, Color.Transparent, null) +
                                "/".CreateColored(Color.White, Color.Transparent, null) +
                                a.Stats.MaxHealth.ToString().CreateColored(Color.DarkGreen, Color.Transparent, null);

            var health = new string((char)176, 14).CreateGradient(Color.Red, Color.GreenYellow, Color.Red, Color.GreenYellow);

            double health_percent;
            if( a.Stats.Health != 0 && a.Stats.MaxHealth != 0 )
            {
                health_percent = (double)a.Stats.Health/(double)a.Stats.MaxHealth;

                //Print(x, y+1, "Health", Color.GreenYellow);
                var sa = a.GetParent().GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation);
                var glyphString = string.Format("{0}:", (char)sa.Animation.CurrentFrame[0].GlyphIndex);
                Print(x, y, glyphString, sa.Animation.CurrentFrame[0].Foreground, sa.Animation.CurrentFrame[0].Background);
                Print(x+glyphString.Length, y, a.GetParent().NAME, Color.White, Color.Transparent);
                Print(Width - healthStatus.ToString().Length, y+1, healthStatus);
                Print(x, y+2, health.SubString(0, (int)((double)health.Count*(double)health_percent)));
                //System.Console.WriteLine("{0}", ((double)health.Count*(double)health_percent));
            }

        }

        private void DrawHealth(ColoredString hs, ColoredString h)
        {
            double health_percent = (double)_health/(double)_maxHealth;

            Print(1, 4, "Health", Color.GreenYellow);
            Print(Width - hs.ToString().Length, 4, hs);
            Print(1, 5, h.SubString(0, (int)((double)h.Count*(double)health_percent)));

            //System.Console.WriteLine("{0}", ((double)health.Count*(double)health_percent));
        }

        private void DrawEnergy(ColoredString es, ColoredString e)
        {

            double energy_percent = (double)_energy/(double)_maxEnergy;
            Print(1, 6, "Energy", Color.GreenYellow);
            Print(Width - es.ToString().Length, 6, es);
            Print(1, 7, e.SubString(0, (int)((double)e.Count*(double)energy_percent)));

            //System.Console.WriteLine("{0}", ((double)energy.Count*(double)energy_percent));
        }

        private void RedrawPanel()
        {
            //TODO: Go Trough and fix this method just copied from older proj.
            this.Clear();
            Print(1, 2, _characterName);
            ColoredString healthStatus = _health.ToString().CreateColored(Color.LightGreen, Color.Black, null) +
                                "/".CreateColored(Color.White, Color.Black, null) +
                                _maxHealth.ToString().CreateColored(Color.DarkGreen, Color.Black, null);
            ColoredString energyStatus = _energy.ToString().CreateColored(Color.LightBlue, Color.Black, null) +
                                "/".CreateColored(Color.White, Color.Black, null) +
                                _maxEnergy.ToString().CreateColored(Color.DarkBlue, Color.Black, null);

            ColoredString health = new string((char)176, 14).CreateGradient(Color.Red, Color.GreenYellow, Color.Red, Color.GreenYellow);
            ColoredString energy = new string((char)176, 14).CreateGradient(Color.Blue, Color.GreenYellow, Color.Blue, Color.GreenYellow);

            if( _health != 0 && _maxHealth != 0 )
            {
                DrawHealth(healthStatus, health);
            }
            if( _energy != 0 && _maxEnergy != 0 )
            {
                DrawEnergy(energyStatus, energy);
            }

            if(targetList.Count > 0)
            {
                Print(4, 10, "Targets");
                var cnt = 0;
                foreach(Actor t in targetList)
                {
                    DrawInfo(1, 11+cnt, t);
                    cnt++;
                }
            }
	    }
    }
}
