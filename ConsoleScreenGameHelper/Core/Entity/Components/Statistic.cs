using System;
using ConsoleScreenGameHelper.EventHandler;
using Newtonsoft.Json;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Statistic : Component
	{

        public override ComponentType ComponentType { get { return ComponentType.Stats; } }
        private event EventHandler<StatsChangedEventArgs> statsChanged;
        public event EventHandler<StatsChangedEventArgs> StatsChanged 
        { 
            add { statsChanged += value; StatusChanged(); }
            remove { statsChanged -= value; StatusChanged(); }
        }
        private int _health;
        private int _maxHealth;
        private int _energy;
        private int _maxEnergy;
        private bool _isInFov;

        [JsonProperty]
        public int Health { get{ return _health; } set{ _health = value; StatusChanged(); } }
        [JsonProperty]
        public int MaxHealth { get{ return _maxHealth; } set{ _maxHealth = value; StatusChanged(); } }
        [JsonProperty]
        public int Energy { get{ return _energy; } set{ _energy = value; StatusChanged(); } }
        [JsonProperty]
        public int MaxEnergy { get{ return _maxEnergy; } set{ _maxEnergy = value; StatusChanged(); } }
        public bool IsInFov { get{ return _isInFov; } set{ _isInFov = value; StatusChanged(); } }

		public Statistic (int health = 100, int maxHealth = 100, int energy = 100, int maxEnergy = 100)
		{
            Health = health;
            MaxHealth = maxHealth;
            Energy = energy;
            MaxEnergy = maxEnergy;

		}
        private void StatusChanged()
        {
            if(statsChanged != null)
            {
                statsChanged(this, new StatsChangedEventArgs(Health, MaxHealth, Energy, MaxEnergy, IsInFov));
            }
        }
        public override void FireEvent(object sender, EventArgs e)
        {

        }
    }
}
