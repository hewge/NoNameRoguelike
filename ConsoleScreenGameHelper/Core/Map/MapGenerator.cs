using System;
using System.Collections.Generic;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Core.Entity.Components;
using SadConsole.Consoles;
using Point = Microsoft.Xna.Framework.Point;
using RogueSharp;
using RogueSharp.MapCreation;
using ConsoleScreenGameHelper.Core.Map.MapObjects;

namespace ConsoleScreenGameHelper.Core.Map
{
	public class MapGenerator<T> where T : RogueSharp.IMap
	{
        MapData dataMap;

        private readonly IMapCreationStrategy<T> mapCreationStrategy;

		public MapGenerator(int width, int height, IMapCreationStrategy<T> mapCreationStrategy, ITextSurfaceRendered textSurface)
		{
            this.mapCreationStrategy = mapCreationStrategy;

            dataMap = new MapData(width, height, textSurface);
		}

        public MapData CreateMap()
        {
            dataMap.Map = mapCreationStrategy.CreateMap();
            dataMap.FieldOfView = new RogueSharp.FieldOfView(dataMap.Map);

            foreach(var cell in dataMap.Map.GetAllCells())
            {
                if(cell.IsWalkable)
                {
					dataMap[cell.X, cell.Y] = new Floor();
                    dataMap[cell.X, cell.Y].RenderToCell(dataMap.textSurface[cell.X, cell.Y], false, false);
                }
                else
                {
                    dataMap.Map.SetCellProperties(cell.X, cell.Y, false, false);
                    dataMap[cell.X, cell.Y] = new Wall();
                    dataMap[cell.X, cell.Y].RenderToCell(dataMap.textSurface[cell.X, cell.Y], false, false);

                    dataMap.Map.SetCellProperties(cell.X, cell.Y, false, cell.IsWalkable);
                }
            }

            return dataMap;
        }
	}
}

