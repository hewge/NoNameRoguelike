using System;

namespace ConsoleScreenGameHelper.EventHandler
{
	public class StatsChangedEventArgs : EventArgs
	{
        //TODO: Maybe add a 'Stat' class that is passed around instead of this.
        public int Health { get; set;}
        public int MaxHealth { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public bool IsInFov { get; set; }

		public StatsChangedEventArgs(int health, int maxHealth, int energy, int maxEnergy, bool isInFov)
		{
            Health = health;
            MaxHealth = maxHealth;
            Energy = energy;
            MaxEnergy = maxEnergy;
            IsInFov = isInFov;
		}
	}
}

