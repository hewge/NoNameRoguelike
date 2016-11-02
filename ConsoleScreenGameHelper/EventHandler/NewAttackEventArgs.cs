using System;
using Microsoft.Xna.Framework;

namespace ConsoleScreenGameHelper.EventHandler
{
	public class NewAttackEventArgs : EventArgs
	{
        public int Damage { get; set; }
        public Point Position { get; set; }

		public NewAttackEventArgs(Point pos, int damage = 0)
		{
            Position = pos;
            Damage = damage;
		}
	}
}

