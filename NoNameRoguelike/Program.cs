using System;
using NoNameRoguelike.Core.Systems;
using Console = SadConsole.Consoles.Console;
using NoNameRoguelike.Core.Screen;
using ConsoleScreenGameHelper.GameComponent;
using SadConsole.Consoles;
using Microsoft.Xna.Framework;

namespace NoNameRoguelike
{
    class Program
    {

        static void Main(string[] args)
        {
            GameWorld.MessageLog = new MessageLog(true);
            GameWorld.GameStateManager = new GameStateManager(SadConsole.Engine.MonoGameInstance, GameWorld.MessageLog);

            SadConsole.Engine.Initialize("Content/Font/C64.font", 80, 45);
            SadConsole.Engine.EngineStart += Engine_EngineStart;
            SadConsole.Engine.EngineUpdated += Engine_EngineUpdated;
            SadConsole.Engine.EngineDrawFrame += Engine_EngineDrawFrame;

            SadConsole.Engine.MonoGameInstance.Components.Add(GameWorld.GameStateManager);
            SadConsole.Engine.Run();
        }

        private static void Engine_EngineStart(object sender, EventArgs e)
        {
            SadConsole.Engine.ConsoleRenderStack.Clear();
            SadConsole.Engine.ActiveConsole = null;
            GameWorld.Start();
        }

        private static void Engine_EngineUpdated(object sender, EventArgs e)
        {
        }

        private static void Engine_EngineDrawFrame(object sender, EventArgs e)
        {
        }
    }
}
