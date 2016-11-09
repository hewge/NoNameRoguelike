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
                    if(Player.GetComponent<Statistic>(ComponentType.Stats).Energy > 0)
                    {
                        ScheduleManager.Add(Player.GetComponent<Actor>(ComponentType.Actor));
                    }
                    IsPlayerTurn = false;
                    TickAll();
                }
            }
            else
            {
                NextAct();
                TickAll();
            }

        }

        private void NextAct()
        {
            IScheduleable scheduleable = ScheduleManager.Get();
            if(scheduleable == null)
            {
                TickAll();
                AddAll();
                return;
            }
            System.Console.WriteLine("{0}", (scheduleable as Actor).GetParent().NAME);
            if((scheduleable as Actor).GetParent() == Player)
            {
                IsPlayerTurn = true;
            }
            else
            {
                var ai = (scheduleable as Actor).GetComponent<ConsoleScreenGameHelper.Core.Entity.Components.AI>(ComponentType.AI);
                ai.Act();
                (scheduleable as Actor).GetParent().Update();
                if((scheduleable as Actor).Stats.Energy > 0)
                {
                    ScheduleManager.Add((scheduleable as Actor));
                }
                NextAct();
            }
        }

        private void TickAll()
        {
            foreach(BaseEntity be in this)
            {
                var s = be.GetComponent<Statistic>(ComponentType.Stats);
                if( s != null)
                {
                    s.Tick();
                }

            }
        }
        private void AddAll()
        {
            foreach(BaseEntity be in this)
            {
                var a = be.GetComponent<Actor>(ComponentType.Actor);
                if( a != null)
                {
                    if(a.Stats.Energy != 0)
                    {
                        ScheduleManager.Add(a);
                    }
                }

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

