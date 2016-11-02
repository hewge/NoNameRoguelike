using System;
using ConsoleScreenGameHelper.GameComponent;
using ConsoleScreenGameHelper.Factory;
using SadConsole;
using Microsoft.Xna.Framework;
using NoNameRoguelike.Core.Systems;
using NoNameRoguelike.Core.Screen;

namespace NoNameRoguelike
{
	public static class GameWorld
	{
        public static GameStateManager GameStateManager;
        public static MessageLog MessageLog;

        public static void Start()
        {
            GameStateManager.ChangeState(new TitleScreen(SadConsole.Engine.MonoGameInstance, GameStateManager));

            GameObjectFactory.LoadBluePrints();
        }
	}
}

