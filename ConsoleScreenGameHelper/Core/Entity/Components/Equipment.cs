using System;
using ConsoleScreenGameHelper.Enum;
using ConsoleScreenGameHelper.Interface;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class Equipment : Component
	{
        //skall detta verkligen vara såhär?
        public IEquipment Head { get; set; }
        public IEquipment Body { get; set; }
        public IEquipment Legs { get; set; }
        public IEquipment Feet { get; set; }
        public IEquipment Hand { get; set; }

        public override ComponentType ComponentType { get { return ComponentType.Equipment; } }

		public Equipment()
		{
		}

        public override void FireEvent(object sender, EventArgs e)
        {

        }

	}
}

