using System;
using ConsoleScreenGameHelper.Interface;
using ConsoleScreenGameHelper.Manager;
using ConsoleScreenGameHelper.Core.Entity.Components;
using System.Collections;
using System.Collections.ObjectModel;

namespace ConsoleScreenGameHelper.Core.Entity
{
	public class EntityContainer : Collection<BaseEntity>
	{
        public ScheduleManager ScheduleManager { get; private set; }
        public BaseEntity Player { get; private set; }
        public bool DidPlayerAct { get; set; }
        public bool IsPlayerTurn { get; set; }
		public EntityContainer()
		{
            ScheduleManager = new ScheduleManager();
		}

        private void GetPlayer()
        {
            foreach(BaseEntity be in this)
            {
                if(be.GetComponent<PlayerInput>(ComponentType.PlayerInput) != null)
                {
                    Player = be;
                    break;
                }
            }
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
            var a = be.GetComponent<Actor>(ComponentType.Actor);
            if(a != null)
            {
                ScheduleManager.Remove(a);
            }
        }

        public void Update()
        {
            if(Player == null)
            {
                GetPlayer();
            }
            DidPlayerAct = false;
            if(IsPlayerTurn)
            {
                Player.Update();
                if(DidPlayerAct == true)
                {
                    IsPlayerTurn = false;
                }
            }
            else
            {
                NextAct();
            }

        }

        private void NextAct()
        {
            IScheduleable scheduleable = ScheduleManager.Get();
            if((scheduleable as Actor).GetParent() == Player)
            {
                IsPlayerTurn = true;
            }
            else
            {
                var ai = (scheduleable as Actor).GetComponent<ConsoleScreenGameHelper.Core.Entity.Components.AI>(ComponentType.AI);
                ai.Act();
                (scheduleable as Actor).GetParent().Update();
                NextAct();
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

