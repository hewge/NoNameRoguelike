using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using ConsoleScreenGameHelper.Interface;

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
            int firstKey = (int)scheduleables.GetKey(0);
            List<IScheduleable> firstGroup = (List<IScheduleable>)scheduleables.GetByIndex(firstKey);
            var firstScheduleable = firstGroup[0];
            System.Console.WriteLine("firstKey:{0}, firstGroup:{1}, firstScheduleable:{2}", firstKey, firstGroup, firstScheduleable);
            firstGroup.Remove(firstScheduleable);
            time = firstKey;
            this.Add(firstScheduleable);
            return firstScheduleable;
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

