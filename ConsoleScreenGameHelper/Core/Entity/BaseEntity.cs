using System;
using Newtonsoft.Json;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.EventHandler;
using ConsoleScreenGameHelper.Interface;
using System.Collections.Generic;

namespace ConsoleScreenGameHelper.Core.Entity
{

	public class BaseEntity
	{
        public Guid ID { get; set; }
        public string NAME = "NoName";
        public ILog logger = null;
        public bool alive = true;
        [JsonProperty]
        private readonly List<Component> _components;

		public BaseEntity()
		{
            _components = new List<Component>();
            ID = Guid.NewGuid();
		}
        public BaseEntity(ILog log) : this()
        {
            this.logger = log;
        }

        public TComponentType GetComponent<TComponentType>(ComponentType componentType) where TComponentType : Component
        {
            return _components.Find(c => c.ComponentType == componentType) as TComponentType;
        }

        public void AddComponent(Component component)
        {
            if(logger != null)
            {
                logger.Debug(string.Format("Component:{0} added to BaseEntity:{1}.", component, this.NAME));
            }
            _components.Add(component);
            component.Initialize(this);
        }

        public void AddComponents(List<Component> components)
        {
            if(logger != null)
            {
                foreach(var component in components)
                {
                    logger.Debug(string.Format("Component:{0} added to BaseEntity:{1}.", component, this.NAME));
                }
            }

            _components.AddRange(components);
            foreach(var component in components)
            {
                component.Initialize(this);
            }
        }

        public void RemoveComponent(Component component)
        {
            if(logger != null)
            {
                logger.Debug(string.Format("Component:{0} removed from BaseEntity:{1}.", component, this.NAME));
            }

            _components.Remove(component);
        }
        public void Die()
        {
            alive = false;
            if(logger != null)
            {
                logger.Debug(string.Format("{0}, Died.", this.NAME));
            }
        }

        public void FireEvent(object sender, EventArgs e)
        {
            if(logger != null)
            {
                //off for now
                //logger.Debug(string.Format("Event:{0} from {1} fired on {2}.", e, sender, this.NAME));
            }
            foreach(var component in _components)
            {
                component.FireEvent(sender, e);
            }
        }
        public void Update()
        {
            if(!alive)
            {
                var a = GetComponent<Actor>(ComponentType.Actor);
                if(a != null)
                {
                   a.Map.MapData.Map.SetCellProperties(a.Sprite.Position.X, a.Sprite.Position.Y, true, true);
                }
 
                _components.RemoveAll(_ => true);
            }
            foreach(var component in _components)
            {
                component.Update();
            }
        }

        public void Render()
        {
            foreach(var component in _components)
            {
                component.Render();
            }
        }
        public override string ToString()
        {
            return string.Format("NAME:{0}, ID:{1}, logger:{2}, alive:{3}, _components:{4}", this.NAME, this.ID, this.logger, this.alive, this._components);
        }
	}
}

