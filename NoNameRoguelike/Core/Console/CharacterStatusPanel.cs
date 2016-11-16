using System;
using SadConsole.Consoles;
using ConsoleScreenGameHelper.Core.Console.Panel;
using System.Collections.Generic;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Console;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.Core.DataContainer;
using ConsoleScreenGameHelper.EventHandler;
using Microsoft.Xna.Framework;
using MathHelper = Microsoft.Xna.Framework.MathHelper;
using SadConsole;

namespace NoNameRoguelike.Core.Console
{
    //TODO: Refactor this class,, should be able to do "Character status ", in a better way.
	public class CharacterStatusPanel : BorderedConsole
	{
        GradientStatusPanel healthStatusPanel;
        GradientStatusPanel energyStatusPanel;

        List<Actor> targetList;

		public CharacterStatusPanel(int width, int height) : base("Status", width, height)
		{
            //TODO: ReDo this whole class in a better way.
            targetList = new List<Actor>();
            healthStatusPanel =  new GradientStatusPanel(null, 14, 2,  Color.Red, Color.GreenYellow);
            energyStatusPanel = new GradientStatusPanel(null, 14, 2, Color.Blue, Color.GreenYellow);
		}

        public void StatusChanged(object sender, EventArgs e)
        {
            if(e.GetType() == typeof(StatsChangedEventArgs))
            {
                                healthStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.health;
                energyStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.energy;
                healthStatusPanel.Update();
                energyStatusPanel.Update();
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
                var glyphString = string.Format("{0}", (char)sa.Animation.CurrentFrame[0].GlyphIndex);
                Print(x, y, glyphString, sa.Animation.CurrentFrame[0].Foreground, sa.Animation.CurrentFrame[0].Background);
                Print(x+glyphString.Length, y, ":"+a.GetParent().NAME, Color.White, Color.Transparent);
                Print(Width - healthStatus.ToString().Length, y+1, healthStatus);
                Print(x, y+2, health.SubString(0, (int)((double)health.Count*(double)health_percent)));
                //System.Console.WriteLine("{0}", ((double)health.Count*(double)health_percent));
            }

        }
        private void RedrawPanel()
        {
            //TODO: Go Trough and fix this method just copied from older proj.
            this.Clear();
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
        public override void Render()
        {
            base.Render();
            if(this.IsVisible == true)
            {
                Renderer.Render((ITextSurfaceRendered)healthStatusPanel, this.Position + new Point(1, 1));
                Renderer.Render((ITextSurfaceRendered)energyStatusPanel, this.Position + new Point(1, 3));
            }
        }
    }
}
