using System;
using ConsoleScreenGameHelper.Core.Map.MapObjects;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.Core.Entity;
using System.Collections.Generic;
using SadConsole.Consoles;
using Microsoft.Xna.Framework;

namespace ConsoleScreenGameHelper.Core.Map
{
	public class MapData
	{

        private RogueSharp.IMap map;
        RogueSharp.Random.IRandom random = new RogueSharp.Random.DotNetRandom();
        MapObjectBase[,] mapData;
        IReadOnlyCollection<RogueSharp.Cell> previousFOV = new List<RogueSharp.Cell>();
        RogueSharp.FieldOfView fieldOfView;
        public ITextSurfaceRendered textSurface;
        int width;
        int height;

        public RogueSharp.FieldOfView FieldOfView { get{ return fieldOfView; } set{ fieldOfView = value; } }
        public RogueSharp.IMap Map { get{ return map; } set{ map = value; } }
        public MapObjectBase this[int x, int y]
        {
            get{ return mapData[x, y]; }
            set{ mapData[x, y] = value; }
        }

		public MapData(int width, int height, ITextSurfaceRendered textSurface)
		{
            this.width = width;
            this.height = height;
            this.textSurface = textSurface;
            mapData = new MapObjectBase[width, height];
		}

        public bool IsWalkable(int x, int y)
        {
            return map.IsWalkable(x, y);
        }

        public void ComputeFOV(BaseEntity be)
        {
            var FOV = be.GetComponent<FOV>(ComponentType.FOV);
            if(FOV != null)
            {
                var A = be.GetComponent<Actor>(ComponentType.Actor);
                if(A != null)
                {
                    foreach(var cell in previousFOV)
                    {
                        this[cell.X, cell.Y].RemoveCellFromView(textSurface[cell.X, cell.Y]);
                    }
                    previousFOV = fieldOfView.ComputeFov(A.Sprite.Position.X, A.Sprite.Position.Y, A.Stats.Awareness, true);
                    foreach(var cell in previousFOV)
                    {
                        Map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                        this[cell.X, cell.Y].RenderToCell(textSurface[cell.X, cell.Y], true, Map.GetCell(cell.X, cell.Y).IsExplored);
                    }
                }
            }
        }

        public Point GetWalkablePosition()
        {
            while(true)
            {
                int x = random.Next(width - 1);
                int y = random.Next(height - 1);

                if(Map.IsWalkable(x, y))
                {
                    return new Point(x, y);
                }
            }
        }



	}
}

