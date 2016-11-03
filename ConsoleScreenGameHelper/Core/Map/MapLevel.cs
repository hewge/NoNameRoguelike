using System;
using System.Linq;
using SadConsole.Consoles;
using RogueSharp.MapCreation;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Entity.Components;

namespace ConsoleScreenGameHelper.Core.Map
{
	public class MapLevel
	{
        MapGenerator<RogueSharp.Map> mapGenerator;

        public MapData MapData { get; set; }
        public EntityContainer EntityContainer { get; set; }

		public MapLevel(int width, int height, IMapCreationStrategy<RogueSharp.Map> mapCreationStrategy, ITextSurfaceRendered textSurface)
		{
            //All this for the generic type argument.
            Type[] typeArgs = mapCreationStrategy.GetType().GetGenericArguments();
            Type genericMapGen = typeof(MapGenerator<>);
            Type constructed = genericMapGen.MakeGenericType(typeArgs);
            mapGenerator = (MapGenerator<RogueSharp.Map>)Activator.CreateInstance(constructed,  width, height, mapCreationStrategy, textSurface );


            EntityContainer = new EntityContainer();
            MapData = mapGenerator.CreateMap();

		}

        public BaseEntity GetEntityAt(int x, int y)
        {
            return EntityContainer.FirstOrDefault(e => e.GetComponent<Actor>(ComponentType.Actor).Sprite.Position.X == x && e.GetComponent<Actor>(ComponentType.Actor).Sprite.Position.Y == y);
        }

        public void Update()
        {
            EntityContainer.Update();
        }

        public void Render()
        {
            foreach(var be in EntityContainer)
            {
                var sa = be.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation);
                if(MapData.FieldOfView.IsInFov(sa.Position.X, sa.Position.Y))
                {
                    be.Render();
                }

            }
        }
	}
}

