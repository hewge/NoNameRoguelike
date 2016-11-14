using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ConsoleScreenGameHelper.Core.Entity
{
	public class ItemContainer : Collection<BaseEntity>
	{
		public ItemContainer()
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

