using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ConsoleScreenGameHelper.GameComponent;
using SadConsole;
using ConsoleScreenGameHelper;

namespace NoNameRoguelike.Core.Screen
{
	public class TitleScreen : ConsoleScreenGameHelper.Core.Screen.BaseGameScreen
	{
        SadConsole.Consoles.TextSurface TitleSplashSurface;
        SadConsole.Consoles.TextSurface PressKeyTextSurface;
        SadConsole.Consoles.TextSurfaceRenderer TitleRenderer;

		public TitleScreen(Game gameRef, GameStateManager manager) : base(gameRef, manager)
		{
            ///Maybe add some Controls and a Controls console for like Options / Playgame button????
            TitleRenderer = new SadConsole.Consoles.TextSurfaceRenderer();
            PressKeyTextSurface = new SadConsole.Consoles.TextSurface(30, 1, SadConsole.Engine.DefaultFont);

            TitleSplashSurface = SadConsole.Readers.REXPaint.Image.Load(File.OpenRead("Content/Splash/nn.xp")).ToTextSurface();
            var editor = new SadConsole.Consoles.SurfaceEditor(PressKeyTextSurface);
            editor.Print(0, 0, "Press SPACE to PLAY THE GAME.");
		}

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //TODO: Detta funkar men skall detta hanteras av en "Input Manager" istället?
                GameWorld.GameStateManager.ChangeState(new DungeonScreen(SadConsole.Engine.MonoGameInstance, GameWorld.GameStateManager));
            }
            base.Update();
        }

        public override void Render()
        {
            base.Render();

            //TODO: Align this correctly some how.
            TitleRenderer.Render(TitleSplashSurface, new Point(15, 2), false);
            TitleRenderer.Render(PressKeyTextSurface, new Point(29, 35), false);

        }
	}
}

