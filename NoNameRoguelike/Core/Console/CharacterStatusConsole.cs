using System;
using ConsoleScreenGameHelper.EventHandler;
using ConsoleScreenGameHelper.Interface;
using ConsoleScreenGameHelper.Core.DataContainer;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.Core.Console.Panel;
using ConsoleScreenGameHelper.Core.Entity;
using SadConsole;
using SadConsole.Consoles;

namespace NoNameRoguelike.Core.Console
{
	public class CharacterStatusConsole : Window
	{
        public BaseEntity StatusConsoleUser { get; set; }
        public Statistic  Statistic { get{ return StatusConsoleUser.GetComponent<Statistic>(ComponentType.Stats); } }

        GradientStatusPanel healthStatusPanel;
        GradientStatusPanel energyStatusPanel;

        StatusPanel strenghtStatusPanel;
        StatusPanel dexterityStatusPanel;
        StatusPanel vitalityStatusPanel;
        StatusPanel intelligenceStatusPanel;
        StatusPanel speedStatusPanel;
        StatusPanel awarenessStatusPanel;
        StatusPanel attackStatusPanel;
        StatusPanel defenceStatusPanel;

        List<IStatusPanel> StatusPanels;

		public CharacterStatusConsole(int width, int height) : base(width, height)
		{
            Title = "Stats";
            CloseOnESC = true;
            Dragable = true;
            ProcessMouseWithoutFocus = true;
            CanFocus = false;
            MouseCanFocus = false;

            StatusPanels = new List<IStatusPanel>();

            healthStatusPanel = new GradientStatusPanel(null, 21, 2, Color.Red, Color.GreenYellow);
            energyStatusPanel = new GradientStatusPanel(null, 21, 2, Color.Blue, Color.GreenYellow);

            strenghtStatusPanel = new StatusPanel(null, 21, 1);
            dexterityStatusPanel = new StatusPanel(null, 21, 1);
            vitalityStatusPanel = new StatusPanel(null, 21, 1);
            intelligenceStatusPanel = new StatusPanel(null, 21, 1);

            speedStatusPanel = new StatusPanel(null, 21, 1);
            awarenessStatusPanel = new StatusPanel(null, 21, 1);

            attackStatusPanel = new StatusPanel(null, 21, 1);
            defenceStatusPanel = new StatusPanel(null, 21, 1);

            StatusPanels.Add(healthStatusPanel);
            StatusPanels.Add(energyStatusPanel);

            StatusPanels.Add(strenghtStatusPanel);
            StatusPanels.Add(dexterityStatusPanel);
            StatusPanels.Add(vitalityStatusPanel);
            StatusPanels.Add(intelligenceStatusPanel);
            StatusPanels.Add(speedStatusPanel);
            StatusPanels.Add(awarenessStatusPanel);
            StatusPanels.Add(attackStatusPanel);
            StatusPanels.Add(defenceStatusPanel);
		}

        public void StatusChanged(object sender, EventArgs e)
        {
            if(e.GetType() == typeof(StatsChangedEventArgs))
            {
                healthStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.health;
                energyStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.energy;
                strenghtStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.strenght;
                dexterityStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.dexterity;
                vitalityStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.vitality;
                intelligenceStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.intelligence;
                speedStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.speed;
                awarenessStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.awareness;
                attackStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.attack;
                defenceStatusPanel.stat = (e as StatsChangedEventArgs).Statistic.defence;
                foreach(var sp in StatusPanels)
                {
                    sp.Update();
                }

            }
        }

        public override void Render()
        {
            base.Render();
            if(this.IsVisible == true)
            {
                int counter = 2;
                foreach(var sp in StatusPanels)
                {
                    Renderer.Render((ITextSurfaceRendered)sp, this.Position + new Point(2, counter));
                    counter+=2;
                }
            }
        }
	}
}

