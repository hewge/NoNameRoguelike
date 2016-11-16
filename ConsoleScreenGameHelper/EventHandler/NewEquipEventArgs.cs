using System;
using ConsoleScreenGameHelper.Core.Entity;

namespace ConsoleScreenGameHelper.EventHandler
{
	public class NewEquipEventArgs : EventArgs
	{
        public BaseEntity BaseEntity { get; set; }
		public NewEquipEventArgs(BaseEntity baseEntity)
		{
            BaseEntity = baseEntity;
		}
	}
}

