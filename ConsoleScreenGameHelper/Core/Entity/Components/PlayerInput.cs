using System;
using ConsoleScreenGameHelper.EventHandler;
using ConsoleScreenGameHelper.Manager;
using ConsoleScreenGameHelper.Enum;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class PlayerInput : Component
	{
        public override ComponentType ComponentType { get { return ComponentType.PlayerInput; } }
		public PlayerInput()
		{
            InputManager.FireNewInput += Move;
		}
        void Move(object sender, EventHandler.NewInputEventArgs e)
        {
            switch (e.Input)
            {
                case Input.Up:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.Up));
                    break;
                case Input.Down:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.Down));
                    break;
                case Input.Right:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.Right));
                    break;
                case Input.Left:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.Left));
                    break;
                case Input.None:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.None));
                    break;
            }
        }

        public override void FireEvent(object sender, EventArgs e)
        {

        }
	}
}

