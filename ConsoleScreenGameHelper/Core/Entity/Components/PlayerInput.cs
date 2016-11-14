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
                case Input.North:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.North));
                    //GetParent().logger.Debug("NORR");
                    break;
                case Input.South:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.South));
                    //GetParent().logger.Debug("SYD");
                    break;
                case Input.East:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.East));
                    //GetParent().logger.Debug("ÖST");
                    break;
                case Input.West:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.West));
                    //GetParent().logger.Debug("VÄST");
                    break;
                case Input.NorthEast:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.NorthEast));
                    break;
                case Input.NorthWest:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.NorthWest));
                    break;
                case Input.SouthEast:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.SouthEast));
                    break;
                case Input.SouthWest:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.SouthWest));
                    break;
                case Input.None:
                    GetParent().FireEvent(this, new NewMoveEventArgs(Direction.None));
                    break;
                case Input.Pickup:
                    GetParent().GetComponent<Components.Inventory>(ComponentType.Inventory).PickupItem();
                    break;
            }
        }

        public override void FireEvent(object sender, EventArgs e)
        {

        }
	}
}

