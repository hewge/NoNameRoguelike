using System;
using ConsoleScreenGameHelper.Utils;
using SadConsole;
using System.Collections.Generic;

namespace ConsoleScreenGameHelper.Core.DataContainer
{
	public class BaseStat
	{
        private int val;
        private int altVal;
        private Dictionary<string, List<int>> modifiers;

        public string Name { get; set; }
        public int Value { get{ return GetTotal(val); } set{ val = value; } }
        public int AltValue { get{ return altVal; } set{ altVal = value; } }
        public Dictionary<string, List<int>> Modifiers { get{ return (Dictionary<string, List<int>>)Copy.DeepClone(modifiers); } set{ modifiers = value; } }

		public BaseStat(string name, int val, int altVal)
		{
            Modifiers = new Dictionary<string, List<int>>();
            Name = name;
            Value = val;
            AltValue = altVal;
		}
        public void AddMod(string typ, int va)
        {
            var l = new List<int>();
            l.Add(va);
            modifiers.Add(typ, l);
        }

        public void AddMod(Dictionary<string, List<int>> mod)
        {
            foreach(var m in mod)
            {
                if(!modifiers.ContainsKey(m.Key))
                {
                    modifiers.Add(m.Key, m.Value);
                }
                else
                {
                    foreach(var v in m.Value)
                    {
                        modifiers[m.Key].Add(v);
                    }
                }
            }
        }

        public void RemoveMod(Dictionary<string, List<int>> mod)
        {
            List<int> adds = new List<int>();
            List<int> muls = new List<int>();
            foreach(var m in mod)
            {
                switch(m.Key)
                {
                    case "add":
                        foreach(var v in m.Value)
                        {
                            adds.Add(v);
                        }
                        break;
                    case "mul":
                        foreach(var v in m.Value)
                        {
                            muls.Add(v);
                        }
                        break;
                }
            }
            if(adds.Count > 0)
            {
                foreach(var a in adds)
                {
                    modifiers["add"].Remove(a);
                }
            }
            if(muls.Count > 0)
            {
                foreach(var m in muls)
                {
                    modifiers["mul"].Remove(m);
                }
            }

        }

        private int GetTotal(int val)
        {
            int total = val;
            int mul = 0;
            foreach(var m in Modifiers)
            {
                switch(m.Key)
                {
                    case "add":
                        foreach(var a in m.Value)
                        {
                            total = total + (int)MathHelper.Clamp(a, 0, 100);
                        }
                        break;
                    case "mul":
                        foreach(var mu in m.Value)
                        {
                            mul = mul + (int)MathHelper.Clamp(mu, 0, 100);
                        }
                        break;
                }
            }
            return total + (total * mul);
        }
	}
}

