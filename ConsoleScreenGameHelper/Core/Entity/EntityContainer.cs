using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace ConsoleScreenGameHelper.Core.Entity
{
	public class EntityContainer : Collection<BaseEntity>
	{
		public EntityContainer()
		{
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

