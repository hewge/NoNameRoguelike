using System;
using RogueSharp.DiceNotation;
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

        //Base Stats.
        private int _strenght;
        private int _speed;
        private int _intelligence;

        //Derrived Stats, from base.

        private int _attack;
        private int _attackChance;
        private int _defence;
        private int _defenceChance;
        private int _awareness;


        private int _health;
        private int _maxHealth;
        private int _energy;
        private int _maxEnergy;
        private bool _isInFov;


        [JsonProperty]
        public int Strenght { get{ return _strenght; } set{ _strenght = value; } }
        [JsonProperty]
        public int Speed { get{ return _speed;} set{ _speed = value; } }
        [JsonProperty]
        public int Intelligence { get{ return _intelligence; } set{ _intelligence = value; } }

        [JsonProperty]
        public int Attack { get{ return _attack;} set{ _attack = value; } }
        [JsonProperty]
        public int AttackChance { get{ return _attackChance; } set{ _attackChance = value; } }
        [JsonProperty]
        public int Defence { get{ return _defence; } set{ _defence = value; } }
        [JsonProperty]
        public int DefenceChance { get{ return _defenceChance; } set{ _defenceChance = value; } }

        [JsonProperty]
        public int Awareness { get{ return _awareness; } set{ _awareness = value; } }

        [JsonProperty]
        public int Health { get{ return _health; } set{ _health = value; StatusChanged(); } }
        [JsonProperty]
        public int MaxHealth { get{ return _maxHealth; } set{ _maxHealth = value; StatusChanged(); } }
        [JsonProperty]
        public int Energy { get{ return _energy; } set{ _energy = value; StatusChanged(); } }
        [JsonProperty]
        public int MaxEnergy { get{ return _maxEnergy; } set{ _maxEnergy = value; StatusChanged(); } }
        public bool IsInFov { get{ return _isInFov; } set{ _isInFov = value; StatusChanged(); } }

        [JsonProperty]
        private bool calculated = false;

        public Statistic() : this(8, 8, 8)
        {

        }
        public Statistic(int str, int spd, int inte)
        {
            _strenght = str;
            _speed = spd;
            _intelligence = inte;
            if(!calculated)
            {
                Calculate();
            }
        }

        private void Calculate()
        {
            DiceExpression diceExpression = new DiceExpression().Constant(_strenght).Dice(3, 2);
            _attack = diceExpression.Roll().Value;
            diceExpression = new DiceExpression().Constant(_strenght).Dice(4, _strenght, 1, choose: 3);
            _health = diceExpression.Roll().Value;
            _maxHealth = _health;
            diceExpression = new DiceExpression().Constant(-11).Dice(3, _intelligence, 1, choose: 3);
            _awareness = Math.Max(diceExpression.Roll().Value, 4);
            diceExpression = new DiceExpression().Constant(_strenght).Dice(3, 2);
            _defence = diceExpression.Roll().Value;
            diceExpression = new DiceExpression().Dice(25+_intelligence, 3, 1, choose: 25);
            _attackChance = diceExpression.Roll().Value;
            diceExpression = new DiceExpression().Dice(10+_intelligence, 4, 1, choose: 15);
            _defenceChance = diceExpression.Roll().Value;
            diceExpression = new DiceExpression().Constant(_strenght).Dice(5+_intelligence, 6);
            _energy = diceExpression.Roll().Value;
            _maxEnergy = _energy;
            StatusChanged();
            calculated = true;
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
