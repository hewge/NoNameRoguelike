using System;
using Newtonsoft.Json;

namespace ConsoleScreenGameHelper.Core.Entity
{
	abstract public class Component : SadConsole.Game.GameObject
	{
        private BaseEntity _baseEntity;
        [JsonProperty]
        public abstract ComponentType ComponentType { get; }

		public Component()
		{
            this.IsVisible = false;
		}

        public abstract void FireEvent(object sender, EventArgs e);

        public virtual void OnAfterInitialize()
        {

        }
        public void Initialize(BaseEntity baseEntity)
        {
            _baseEntity = baseEntity;
            OnAfterInitialize();
        }

        public Guid GetOwnerID()
        {
            return _baseEntity.ID;
        }
        public BaseEntity GetParent()
        {
            return _baseEntity;
        }

        public void RemoveMe()
        {
            _baseEntity.RemoveComponent(this);
        }

        public override string ToString()
        {
            return string.Format("{0}", ComponentType);
        }


        public TComponentType GetComponent<TComponentType>(ComponentType componentType) where TComponentType : Component
        {
            return _baseEntity.GetComponent<TComponentType>(componentType);
        }
	}
}

