using System;
using ConsoleScreenGameHelper.Enum;
using ConsoleScreenGameHelper.Core.DataContainer;
using Microsoft.Xna.Framework;
using RogueSharp.DiceNotation;
using ConsoleScreenGameHelper.EventHandler;
using Newtonsoft.Json;

namespace ConsoleScreenGameHelper.Core.Entity.Components {
	public class Statistic : Component
	{

        public override ComponentType ComponentType { get { return ComponentType.Stats; } }
        private event EventHandler<StatsChangedEventArgs> statsChanged;
        public event EventHandler<StatsChangedEventArgs> StatsChanged 
        { 
            add { statsChanged += value; StatusChanged(); }
            remove { statsChanged -= value; StatusChanged(); }
        }
        //Buffering for regeneration
        double _health_buffer;
        double _energy_buffer;

        private double _hpRegen;
        private double _enRegen;

        [JsonProperty]
        public BaseStat strenght;
        [JsonProperty]
        public BaseStat dexterity;
        [JsonProperty]
        public BaseStat vitality;
        [JsonProperty]
        public BaseStat intelligence;

        [JsonProperty]
        public BaseStat speed;
        [JsonProperty]
        public BaseStat awareness; 
        [JsonProperty]
        public BaseStat attack;
        [JsonProperty]
        public BaseStat defence;

        [JsonProperty]
        public BaseStat energy;
        [JsonProperty]
        public BaseStat health;

        public int Health { get{ return health.Value; } set{ health.Value = MathHelper.Clamp(value, 0, health.AltValue); StatusChanged();} }
        public int MaxHealth { get{ return health.AltValue; } set{ health.AltValue = value;StatusChanged(); } }
        public int Energy { get{ return energy.Value; } set{ energy.Value = MathHelper.Clamp(value, 0, energy.AltValue);StatusChanged(); } }
        public int MaxEnergy { get{ return energy.AltValue; } set{ energy.AltValue = value;StatusChanged(); } }
        public int Strenght { get{ return strenght.Value; } set{ strenght.Value = value;StatusChanged(); } }
        public int Dexterity { get{ return dexterity.Value; } set{ dexterity.Value = value;StatusChanged(); } }
        public int Vitality { get{ return vitality.Value; } set{ vitality.Value = value;StatusChanged(); } }
        public int Intelligence { get{ return intelligence.Value; } set{ intelligence.Value = value;StatusChanged(); } }

        public int Speed { get{ return speed.Value; } set{ speed.Value = value;StatusChanged(); } }
        public int Awareness { get{ return awareness.Value; } set{ awareness.Value = value;StatusChanged(); } }

        public int Attack { get{ return attack.Value; } set{ attack.Value = value;StatusChanged(); } }
        public int AttackChance { get{ return attack.AltValue; } set{ attack.AltValue = value;StatusChanged(); } }
        public int Defence { get{ return defence.Value; } set{ defence.Value = value;StatusChanged(); } }
        public int DefenceChance { get{ return defence.AltValue; } set{ defence.AltValue = value;StatusChanged(); } }

        [JsonProperty]
        public double HealthRegeneration { get{ return _hpRegen; } set{ _hpRegen = value; StatusChanged(); } }
        [JsonProperty]
        public double EnergyRegeneration { get{ return _enRegen; } set{ _enRegen = value; StatusChanged(); } }

        public bool IsInFov { get{ return GetComponent<Actor>(ComponentType.Actor).Map.MapData.FieldOfView.IsInFov(GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position.X, GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position.Y); } set{ StatusChanged(); }}

        public EquipmentSlot EquipmentSlot { get; set; }

        [JsonProperty]
        private bool calculated = false;

        public Statistic(EquipmentSlot equipmentSlot) : this(0, 0, 0, 0)
        {
            EquipmentSlot = equipmentSlot;
        }

        public Statistic() : this(8, 8, 8, 8)
        {

        }
        public Statistic(int str, int dex, int vit, int inte)
        {
            health = new BaseStat("Health", 1,  100);
            energy = new BaseStat("Energy", 0, 100);

            strenght = new BaseStat("Strenght", 0, 0);
            dexterity = new BaseStat("Dexterity", 0, 0);
            vitality = new BaseStat("Vitality", 0, 0);
            intelligence = new BaseStat("Intelligence", 0, 0);
            speed = new BaseStat("Speed", 0, 0);

            awareness = new BaseStat("Awareness", 0, 0);
            attack = new BaseStat("Attack", 0, 0);
            defence = new BaseStat("Defence", 0, 0);

            Strenght = str;
            Dexterity = dex;
            Vitality = vit;
            Intelligence = inte;
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
            if(Strenght == 0)
            {
                //This is an item do not calculate.
                calculated = true;
                return;
            }
            //Not an Equipment
            EquipmentSlot = EquipmentSlot.None;

            for(int i=0; i<Vitality;i++)
            {
                DiceExpression diceExpression = new DiceExpression().Constant(1).Dice(1, 2);
                Health+=diceExpression.Roll().Value;
                Energy+=diceExpression.Roll().Value;
            }
            MaxHealth = Health;
            MaxEnergy = Energy;
            double atk=0;
            for(int i=0;i<Strenght;i++)
            {
                DiceExpression diceExpression = new DiceExpression().Dice(1, 2);
                atk+=diceExpression.Roll().Value/2.0;
            }
            Attack=(int)atk;
            double def=0;
            for(int i=0;i<Strenght;i++)
            {
                DiceExpression diceExpression = new DiceExpression().Dice(1, 2);
                def+=(diceExpression.Roll().Value/2.0)*0.80;
            }
            Defence=(int)def;
            double atk_c = 50.0+(Intelligence*0.3);
            for(int i=0;i<Dexterity;i++)
            {
                DiceExpression diceExpression = new DiceExpression().Dice(2, 3, 1, choose: 1);
                atk_c+=diceExpression.Roll().Value;
            }
            AttackChance=(int)atk_c;
            double def_c = 20.0+(Intelligence*0.3);
            for(int i=0;i<Dexterity;i++)
            {
                DiceExpression diceExpression = new DiceExpression().Dice(2, 3, 1, choose: 1);
                def_c+=diceExpression.Roll().Value;
            }
            DefenceChance=(int)def_c;
            Awareness=(int)(2.0+(Intelligence*0.3));
            Speed=(int)(15.0-(Dexterity*0.4));
            _hpRegen=(Vitality+Intelligence)*0.005;
            _enRegen=(Vitality+Intelligence)*0.041;

            StatusChanged();
            calculated = true;
        }

        public void Tick()
        {
            if(Health!=MaxHealth)
                _health_buffer += (double)HealthRegeneration;
            if(Energy!=MaxEnergy)
                _energy_buffer += (double)EnergyRegeneration;

            while(_health_buffer >= 1)
            {
                Health += 1;
                _health_buffer -= 1D;
            }
            while(_energy_buffer >= 1)
            {
                Energy += 1;
                _energy_buffer -= 1D;
            }

            System.Console.WriteLine("HealthBuffer:{0:0.##}, EnergyBuffer:{1:0.##}, for {2}.", _health_buffer, _energy_buffer, GetParent().NAME);
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
                statsChanged(this, new StatsChangedEventArgs(this));
            }
        }
        public override void FireEvent(object sender, EventArgs e)
        {

        }
    }
}
