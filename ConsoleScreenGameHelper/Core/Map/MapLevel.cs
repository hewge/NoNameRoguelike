﻿using System;
using System.Collections;
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

        public readonly int Width;
        public readonly int Height;

        private Stack cameraFollow = new Stack();
        //Right now this is and must be only player.
        public BaseEntity CameraFollow { get{ return (BaseEntity)cameraFollow.Peek(); } set{ cameraFollow.Push(value); } }

		public MapLevel(int width, int height, IMapCreationStrategy<RogueSharp.Map> mapCreationStrategy, ITextSurfaceRendered textSurface)
		{
            Width = width;
            Height = height;
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
            if(CameraFollow != null)
            {
                MapData.ComputeFOV(CameraFollow);
            }

            EntityContainer.Update();
        }

        public void Render()
        {
            foreach(var be in EntityContainer)
            {
                var sa = be.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation);
                if(MapData.FieldOfView.IsInFov(sa.Position.X, sa.Position.Y))
                {
                    //TODO: fix in fov code. true/or false does not matter, just makes the status panel redraw.
                    be.GetComponent<Statistic>(ComponentType.Stats).IsInFov = true;
                    be.Render();
                }
                else
                {
                    be.GetComponent<Statistic>(ComponentType.Stats).IsInFov = false;
                }
            }
        }
	}
}

