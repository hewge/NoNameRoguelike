using System;
using RogueSharp;
using Point = Microsoft.Xna.Framework.Point;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Interface;

namespace ConsoleScreenGameHelper.Core.Entity.AI.Behaviour
{
	public class Roam : IBehaviour
	{
        BaseEntity parent;
		public Roam(BaseEntity parent)
		{
            this.parent = parent;
		}
        public bool Act()
        {
            var ac = parent.GetComponent<Actor>(ComponentType.Actor);
            var ai = parent.GetComponent<ConsoleScreenGameHelper.Core.Entity.Components.AI>(ComponentType.AI);
            var pos = parent.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position;
            var playerPos = ac.Map.CameraFollow.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position;
            IMap map = ac.Map.MapData.Map;

            if(!ai.TurnsAlerted.HasValue && ac.Stats.Health == ac.Stats.MaxHealth)
            {

                ac.Map.MapData.SetIsWalkable(pos.X, pos.Y, true);
                ac.Map.MapData.SetIsWalkable(playerPos.X, playerPos.Y, true);

                PathFinder pathFinder = new PathFinder(map);
                Path path = null;
                try
                {
                    Point dest = ac.Map.MapData.GetWalkablePosition();
                    path = pathFinder.ShortestPath(map.GetCell(pos.X, pos.Y), map.GetCell(dest.X, dest.Y));
                }
                catch(PathNotFoundException)
                {
                    System.Console.WriteLine("PathNotFound while Roaming");
                }



                ac.Map.MapData.SetIsWalkable(pos.X, pos.Y, false);
                ac.Map.MapData.SetIsWalkable(playerPos.X, playerPos.Y, false);

                if(path != null)
                {
                    ac.Sprite.Move(path.StepForward());
                }

                return true;

            }
            return false;
        }
    }
}

