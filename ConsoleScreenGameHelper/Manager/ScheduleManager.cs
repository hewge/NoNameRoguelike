using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using ConsoleScreenGameHelper.Interface;
using ConsoleScreenGameHelper.Core.Entity.Components;

namespace ConsoleScreenGameHelper.Manager
{
	public class ScheduleManager
	{
        private int time;
        private readonly SortedList scheduleables;

		public ScheduleManager ()
		{
            time = 0;
            scheduleables = new SortedList();
		}

        public void Add(IScheduleable scheduleable)
        {
            int key = time + scheduleable.Time;
            if(!scheduleables.ContainsKey(key))
            {
                scheduleables.Add(key, new List<IScheduleable>());
            }
            (scheduleables[key] as List<IScheduleable>).Add(scheduleable);
        }

        public void Add(IScheduleable scheduleable, int extratime)
        {
            int key = time + scheduleable.Time + extratime;
            if(!scheduleables.ContainsKey(key))
            {
                scheduleables.Add(key, new List<IScheduleable>());
            }
            (scheduleables[key] as List<IScheduleable>).Add(scheduleable);
        }

        public void Remove(IScheduleable scheduleable)
        {

            DictionaryEntry scheduleableListFound = new DictionaryEntry();
            foreach(DictionaryEntry scheduleableList in scheduleables)
            {
                if(((scheduleableList).Value as List<IScheduleable>).Contains(scheduleable))
                {
                    scheduleableListFound = (DictionaryEntry)scheduleableList;
                    break;
                }
            }
            if(scheduleableListFound.Value != null)
            {
                (scheduleableListFound.Value as List<IScheduleable>).Remove(scheduleable);
                if((scheduleableListFound.Value as List<IScheduleable>).Count <= 0)
                {
                    scheduleables.Remove(scheduleableListFound.Key);
                }
            }
        }

        public IScheduleable Get()
        {
            if(scheduleables.Count > 0)
            {
                int firstKey = (int)scheduleables.GetKey(0);
                List<IScheduleable> firstGroup = (List<IScheduleable>)scheduleables[firstKey];
                var firstScheduleable = firstGroup[0];
                this.Remove(firstScheduleable);
                time = firstKey;
                return firstScheduleable;
            }
            return null;
        }

        public int GetTime()
        {
            return time;
        }

        public void Clear()
        {
            time = 0;
            scheduleables.Clear();
        }
	}
}

