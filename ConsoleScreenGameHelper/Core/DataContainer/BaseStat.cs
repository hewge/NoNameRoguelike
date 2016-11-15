using System;

namespace ConsoleScreenGameHelper.Core.DataContainer
{
	public class BaseStat
	{
        public string Name { get; set; }
        public int Value { get; set; }
        public int AltValue { get; set; }

		public BaseStat(string name, int val, int altVal)
		{
            Name = name;
            Value = val;
            AltValue = altVal;
		}
	}
}

