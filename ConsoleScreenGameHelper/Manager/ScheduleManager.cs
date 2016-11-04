﻿using System;
using ConsoleScreenGameHelper.Interface;
using System.Collections.Generic;

namespace ConsoleScreenGameHelper.Manager
{
	public class ScheduleManager
	{
        private int time;
        private readonly SortedDictionary<int, List<IScheduleable>> scheduleables;

		public ScheduleManager ()
		{
            time = 0;
            scheduleables = new SortedDictionary<int, List<IScheduleable>>();
		}

        public void Add(IScheduleable scheduleable)
        {
            int key = time + scheduleables.Time;
            if(!scheduleables.ContainsKey(key))
            {
                scheduleables.Add(key, new List<IScheduleable>());
            }
            scheduleables[key].Add(scheduleable);
        }

        public void Remove(IScheduleable scheduleable)
        {
            KeyValuePair<int, List<IScheduleable>> scheduleableListFound = new KeyValuePair<int, List<IScheduleable>>(-1, null);
            foreach(var scheduleableList in scheduleables)
            {
                if(scheduleableList.Value.Contains(scheduleable))
                {
                    scheduleableListFound = scheduleableList;
                    break;
                }
            }
            if(scheduleableListFound.Value != null)
            {
                scheduleableListFound.Value.Remove(scheduleable);
                if(scheduleableListFound.Value.Count <= 0)
                {
                    scheduleables.Remove(scheduleableListFound.Key);
                }
            }
        }

        public IScheduleable Get()
        {
            var firstScheduleableGroup = scheduleables.First();
            var firstScheduleable = firstScheduleableGroup.Value.First();
            Remove(firstScheduleable);
            time = firstScheduleableGroup.Key;
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
