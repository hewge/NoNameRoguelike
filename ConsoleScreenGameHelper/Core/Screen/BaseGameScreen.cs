using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameStateManager = ConsoleScreenGameHelper.GameComponent.GameStateManager;
using SadConsole.Input;
using ConsoleScreenGameHelper.Core.Console;

namespace ConsoleScreenGameHelper.Core.Screen
{
	public abstract partial class BaseGameScreen : GameState
	{
        protected Game GameRef;
        protected ContentManager Content;
		public BaseGameScreen(Game gameRef, GameStateManager manager) : base(gameRef, manager)
		{
            GameRef = gameRef;
		}
        protected virtual void LoadContent()
        {
            Content = GameRef.Content;
        }
        public override bool ProcessKeyboard(KeyboardInfo info)
        {
                return false;
        }
	}
}

