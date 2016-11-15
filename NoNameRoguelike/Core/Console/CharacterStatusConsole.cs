using System;
using ConsoleScreenGameHelper.EventHandler;
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

        StatusPanel statusPanel;

		public CharacterStatusConsole(int width, int height) : base(width, height)
		{
            Title = "Stats";
            CloseOnESC = true;
            Dragable = true;
            ProcessMouseWithoutFocus = true;
            CanFocus = false;
            MouseCanFocus = false;

            statusPanel = new StatusPanel(new KeyValuePair<string, int>("Hej", 10), 10, 10);
		}

        public void StatusChanged(object sender, EventArgs e)
        {
            if(e.GetType() == typeof(StatsChangedEventArgs))
            {

            }
        }

        private void RedrawPanel()
        {
        }

        public override void Render()
        {
            base.Render();
            if(this.IsVisible == true)
            {
                Renderer.Render(statusPanel, this.Position + new Point(3, 3));
            }
        }
	}
}

