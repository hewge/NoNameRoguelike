using System;

namespace ConsoleScreenGameHelper.EventHandler
{
	public class NewDamageEventArgs : EventArgs
	{
        public int Damage { get; set; }

		public NewDamageEventArgs(int damage)
		{
            Damage = damage;
		}
	}
}

