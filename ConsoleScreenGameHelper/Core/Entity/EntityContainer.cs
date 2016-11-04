using System;
using ConsoleScreenGameHelper.Interface;
using ConsoleScreenGameHelper.Manager;
using System.Collections;
using System.Collections.ObjectModel;

namespace ConsoleScreenGameHelper.Core.Entity
{
	public class EntityContainer : Collection<BaseEntity>
	{
        public ScheduleManager ScheduleManager { get; private set; }
		public EntityContainer()
		{
            ScheduleManager = new ScheduleManager();
		}

        public new void Add(BaseEntity be)
        {
            base.Add(be);
            var a = be.GetComponent<Actor>(ComponentType.Actor);
            if(a != null)
            {
                ScheduleManager.Add(a);
            }
        }

        public new void Remove(BaseEntity be)
        {
            base.Remove(be);
            var a = GetComponent<Actor>(ComponentType.Actor);
            if(a != null)
            {
                ScheduleManager.Remove(a);
            }
        }

        public void Update()
        {
            foreach(BaseEntity be in this)
            {
                be.Update();
            }
        }

        public void Render()
        {
            //FOV
            /*
            foreach(BaseEntity be in this)
            {
                be.Render();
            }
            */
        }
	}
}

