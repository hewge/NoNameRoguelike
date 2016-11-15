using System;
using ConsoleScreenGameHelper.Core.Entity.Components;

namespace ConsoleScreenGameHelper.EventHandler
{
	public class StatsChangedEventArgs : EventArgs
	{
        //TODO: Maybe add a 'Stat' class that is passed around instead of this.
        public int Health { get; set;}
        public int MaxHealth { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int Strenght { get; set; }
        public int Dexterity { get; set; }
        public int Vitality { get; set; }
        public int Intelligence { get; set; }
        public int Attack { get; set; }
        public int AttackChance { get; set; }
        public int Defence { get; set; }
        public int DefenceChance { get; set; }
        public int Speed { get; set; }
        public int Awareness { get; set; }
        public double HealthRegeneration { get; set; }
        public double EnergyRegeneration { get; set; }
        public bool IsInFov { get; set; }

		public StatsChangedEventArgs(Statistic stats)
		{
            Health = stats.Health;
            MaxHealth = stats.MaxHealth;
            Energy = stats.Energy;
            MaxEnergy = stats.MaxEnergy;
            IsInFov = stats.IsInFov;
            Strenght = stats.Strenght;
            Dexterity = stats.Dexterity;
            Vitality = stats.Vitality;
            Intelligence = stats.Intelligence;
            Attack = stats.Attack;
            AttackChance = stats.AttackChance;
            Defence = stats.Defence;
            DefenceChance = stats.DefenceChance;
            Speed = stats.Speed;
            Awareness = stats.Awareness;
            HealthRegeneration = stats.HealthRegeneration;
            EnergyRegeneration = stats.EnergyRegeneration;
		}
	}
}

