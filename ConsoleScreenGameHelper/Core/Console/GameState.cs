using System;
using Microsoft.Xna.Framework;
using SadConsole.Consoles;
using ConsoleScreenGameHelper.GameComponent;

namespace ConsoleScreenGameHelper.Core.Console
{
	public abstract partial class GameState : ConsoleList
	{
        GameState tag;
        public GameState Tag
        { get{return tag;} }

        protected GameStateManager StateManager;

		public GameState(Game gameRef, GameStateManager manager) : base()
		{
            tag = this;
            StateManager = manager;
		}
        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            if(StateManager.CurrentState == Tag)
            {
                Show();
            }
            else
            {
                 Hide();
            }
        }

        protected virtual void Show()
        {
            IsVisible = true;
            SadConsole.Engine.ActiveConsole = this;
            var consoles = GetEnumerator();
            while(consoles.MoveNext())
            {
                 (consoles.Current as SadConsole.Consoles.IConsole).IsVisible = true;
                 (consoles.Current as SadConsole.Consoles.IConsole).DoUpdate = true;
            }
            SadConsole.Engine.ConsoleRenderStack.Add(this);
        }
        protected virtual void Hide()
        {
            IsVisible = false;
            IsFocused = false;
            if(StateManager.CurrentState != null)
            {
                 SadConsole.Engine.ActiveConsole = StateManager.CurrentState;
            }
            var consoles = GetEnumerator();
            while(consoles.MoveNext())
            {
                 ((SadConsole.Consoles.Console)consoles.Current).IsVisible = false;
                 ((SadConsole.Consoles.Console)consoles.Current).DoUpdate = false;
            }
            SadConsole.Engine.ConsoleRenderStack.Remove(this);
        }
	}
}

