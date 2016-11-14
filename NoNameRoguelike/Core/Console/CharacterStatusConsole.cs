using System;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.Core.Entity;
using SadConsole;
using SadConsole.Consoles;

namespace NoNameRoguelike.Core.Console
{
	public class CharacterStatusConsole : Window
	{
        public BaseEntity StatusConsoleUser { get; set; }
        public Statistic  Statistic { get{ return StatusConsoleUser.GetComponent<Statistic>(ComponentType.Stats); } }

		public CharacterStatusConsole(int width, int height) : base(width, height)
		{
            Title = "Stats";
            CloseOnESC = true;
            Dragable = true;
            ProcessMouseWithoutFocus = true;
            CanFocus = false;
            MouseCanFocus = false;
		}
	}
}

