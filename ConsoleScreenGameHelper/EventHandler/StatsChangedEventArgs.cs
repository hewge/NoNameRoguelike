using System;
using ConsoleScreenGameHelper.Core.Entity.Components;

namespace ConsoleScreenGameHelper.EventHandler
{
	public class StatsChangedEventArgs : EventArgs
	{
        //TODO: Maybe add a 'Stat' class that is passed around instead of this.
        public Statistic Statistic { get; set; }

		public StatsChangedEventArgs(Statistic stats)
		{
            Statistic = stats;
		}
	}
}

