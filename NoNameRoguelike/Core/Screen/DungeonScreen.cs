using System;
using SadConsole;
using SadConsole.Input;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.Core.Screen;
using ConsoleScreenGameHelper.GameComponent;
using NoNameRoguelike.Core.Console;
using NoNameRoguelike.Core.Systems;
using Microsoft.Xna.Framework;

namespace NoNameRoguelike.Core.Screen
{
	class DungeonScreen : BaseGameScreen
	{
        public DungeonViewConsole dungeonViewConsole;
        public CharacterStatusPanel characterStatusPanel;
        public MessageConsole messageConsole;

		public DungeonScreen(Game gameRef, GameStateManager manager) : base(gameRef, manager)
		{
            //TODO: Get these values dynamically.
            int width = 80;
            int height = 45;

            messageConsole = new MessageConsole((int)(width), (int)(height*0.30));
            Add(messageConsole);
            dungeonViewConsole = new DungeonViewConsole((int)(width*0.80), (int)(height*0.70), 30, 30);
            characterStatusPanel = new CharacterStatusPanel((int)(width*0.20), (int)(height*0.70));

            characterStatusPanel.Position = new Point((int)(width*0.80), 0);
            messageConsole.Position = new Point(0, (int)(height*0.70));

            Add(characterStatusPanel);
            Add(dungeonViewConsole);

            //TODO: Find a way to init those dynamically.

            dungeonViewConsole.Player.GetComponent<Actor>(ComponentType.Actor).Stats.StatsChanged += characterStatusPanel.StatusChanged;
            dungeonViewConsole.mapLevel.EntityContainer[0].GetComponent<Actor>(ComponentType.Actor).Stats.StatsChanged += characterStatusPanel.actor_StatusChanged;
            characterStatusPanel.CharacterName = dungeonViewConsole.Player.NAME;

		}

        public override bool ProcessKeyboard(KeyboardInfo info)
        {
            return dungeonViewConsole.ProcessKeyboard(info);
        }
	}
}

