using System;
using ConsoleScreenGameHelper.Core.Entity.Components;
using Point = Microsoft.Xna.Framework.Point;
using RogueSharp;
using ConsoleScreenGameHelper.Interface;


namespace ConsoleScreenGameHelper.Core.Entity.AI.Behaviour
{
	public class MoveAndAttack : IBehaviour
	{
        BaseEntity parent;
		public MoveAndAttack(BaseEntity parent)
		{
            this.parent = parent;
		}

        public bool Act()
        {
            var ac = parent.GetComponent<Actor>(ComponentType.Actor);
            var ai = parent.GetComponent<Components.AI>(ComponentType.AI);
            var pos = parent.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position;
            IMap map = ac.Map.MapData.Map;
            Point playerPos = ac.Map.CameraFollow.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position;
            FieldOfView fov = new FieldOfView(map);
            if(!ai.TurnsAlerted.HasValue)
            {
                fov.ComputeFov(pos.X, pos.Y, ac.Stats.Awareness , true);
                if(fov.IsInFov(playerPos.X, playerPos.Y))
                {
                    System.Console.WriteLine("MoveAndAttack - Player Found!");
                    if(parent.logger != null)
                    {
                        parent.logger.Write(string.Format("{0}, Charges forward towards {1}.", parent.NAME, ac.Map.CameraFollow.NAME));
                    }


                    ai.TurnsAlerted = 1;
                }
            }
            if(ai.TurnsAlerted.HasValue)
            {
                ac.Map.MapData.SetIsWalkable(pos.X, pos.Y, true);
                ac.Map.MapData.SetIsWalkable(playerPos.X, playerPos.Y, true);

                PathFinder pathFinder = new PathFinder(map);
                Path path = null;

                try
                {
                    path = pathFinder.ShortestPath(map.GetCell(pos.X, pos.Y), map.GetCell(playerPos.X, playerPos.Y));
                }
                catch(PathNotFoundException)
                {

                    //(message in logger?)
                    if(parent.logger != null)
                    {
                        parent.logger.Write(string.Format("{0}, Can't seem to find {1}.", parent.NAME, ac.Map.CameraFollow.NAME));
                    }
                    System.Console.WriteLine("MoveAndAttack - PathNotFoundException");
                }

                ac.Map.MapData.SetIsWalkable(pos.X, pos.Y, false);
                ac.Map.MapData.SetIsWalkable(playerPos.X, playerPos.Y, false);

                if(path != null)
                {
                    try
                    {
                        ac.Sprite.Move(path.CurrentStep);
                        path.StepForward();
                    }
                    catch(NoMoreStepsException)
                    {
                        System.Console.WriteLine("MoveAndAttack - NoMoreStepsException");
                        if(parent.logger != null)
                        {
                            //parent.logger.Debug(string.Format("{0}, Can't to advance towards {1}.", parent.NAME, ac.Map.CameraFollow.NAME));
                        }

                    }
                }
                ai.TurnsAlerted++;

                if(ai.TurnsAlerted > 15)
                {
                    ai.TurnsAlerted = null;
                }
            }
            return true;
        }
	}
}

