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
        private int _dexterity;
        private int _vitality;
        private int _intelligence;

        //Derrived Stats, from base.

        private int _attack;
        private int _attackChance;
        private int _defence;
        private int _defenceChance;
        private int _awareness;

        private double _hpRegen;
        private double _enRegen;

        private int _speed;

        private int _health;
        private int _maxHealth;
        private int _energy;
        private int _maxEnergy;


        [JsonProperty]
        public int Strenght { get{ return _strenght; } set{ _strenght = value; } }
        [JsonProperty]
        public int Dexterity { get{ return _dexterity; } set{ _dexterity = value; } }
        [JsonProperty]
        public int Vitality { get{ return _vitality; } set{ _vitality = value; } }
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
        public int Speed { get{ return _speed;} set{ _speed = value; } }
        [JsonProperty]
        public int Awareness { get{ return _awareness; } set{ _awareness = value; } }

        [JsonProperty]
        public double HealthRegeneration { get{ return _hpRegen; } set{ _hpRegen = value; } }
        [JsonProperty]
        public double EnergyRegeneration { get{ return _enRegen; } set{ _enRegen = value; } }

        [JsonProperty]
        public int Health { get{ return _health; } set{ _health = value; StatusChanged(); } }
        [JsonProperty]
        public int MaxHealth { get{ return _maxHealth; } set{ _maxHealth = value; StatusChanged(); } }
        [JsonProperty]
        public int Energy { get{ return _energy; } set{ _energy = value; StatusChanged(); } }
        [JsonProperty]
        public int MaxEnergy { get{ return _maxEnergy; } set{ _maxEnergy = value; StatusChanged(); } }
        public bool IsInFov { get{ return GetComponent<Actor>(ComponentType.Actor).Map.MapData.FieldOfView.IsInFov(GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position.X, GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position.Y); } set{ StatusChanged(); }}

        [JsonProperty]
        private bool calculated = false;

        public Statistic() : this(8, 8, 8, 8)
        {

        }
        public Statistic(int str, int dex, int vit, int inte)
        {
            _strenght = str;
            _dexterity = dex;
            _vitality = vit;
            _intelligence = inte;
            if(!calculated)
            {
                Calculate();
            }
        }


        private void Calculate()
        {
            //Old calculation with str, int and speed
            /*
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
            */

            for(int i=0; i<_vitality;i++)
            {
                DiceExpression diceExpression = new DiceExpression().Constant(1).Dice(1, 2);
                _health+=diceExpression.Roll().Value;
                _energy+=diceExpression.Roll().Value;
            }
            _maxHealth = _health;
            _maxEnergy = _energy;
            float atk=0;
            for(int i=0;i<_strenght;i++)
            {
                DiceExpression diceExpression = new DiceExpression().Dice(1, 2);
                atk+=diceExpression.Roll().Value/2;
            }
            _attack=(int)atk;
            double def=0;
            for(int i=0;i<_strenght;i++)
            {
                DiceExpression diceExpression = new DiceExpression().Dice(1, 2);
                def+=(diceExpression.Roll().Value/2)*0.80;
            }
            _defence=(int)def;
            double atk_c = 50+(_intelligence*0.3);
            for(int i=0;i<_dexterity;i++)
            {
                DiceExpression diceExpression = new DiceExpression().Dice(2, 3, 1, choose: 1);
                atk_c+=diceExpression.Roll().Value;
            }
            _attackChance=(int)atk_c;
            double def_c = 20+(_intelligence*0.3);
            for(int i=0;i<_dexterity;i++)
            {
                DiceExpression diceExpression = new DiceExpression().Dice(2, 3, 1, choose: 1);
                def_c+=diceExpression.Roll().Value;
            }
            _defence=(int)def_c;
            _awareness=(int)(2+(_intelligence*0.3));
            _speed=(int)(15-(_dexterity*0.4));
            _hpRegen=(_vitality+_intelligence)*0.022;
            _enRegen=(_vitality+_intelligence)*0.053;

            StatusChanged();
            calculated = true;
        }

        private void StatusChanged()
        {
            if(Health <= 0)
            {
                var a = GetComponent<Actor>(ComponentType.Actor);
                a.Die();
            }
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
