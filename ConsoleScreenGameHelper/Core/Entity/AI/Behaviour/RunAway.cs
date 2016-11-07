using System;
using RogueSharp;
using Point = Microsoft.Xna.Framework.Point;
using ConsoleScreenGameHelper.Core.Entity.Components;
using ConsoleScreenGameHelper.Core.Entity;
using ConsoleScreenGameHelper.Interface;

namespace ConsoleScreenGameHelper.Core.Entity.AI.Behaviour
{
	public class RunAway : IBehaviour
	{
        BaseEntity parent;

		public RunAway(BaseEntity parent)
		{
            this.parent = parent;
		}

        public bool Act()
        {
            var ac = parent.GetComponent<Actor>(ComponentType.Actor);
            var pos = parent.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position;
            IMap map = ac.Map.MapData.Map;
            Point playerPos = ac.Map.CameraFollow.GetComponent<SpriteAnimation>(ComponentType.SpriteAnimation).Position;

            ac.Map.MapData.SetIsWalkable(pos.X, pos.Y, true);
            ac.Map.MapData.SetIsWalkable(playerPos.X, playerPos.Y, true);

            GoalMap goalMap = new GoalMap(map);
            goalMap.AddGoal(playerPos.X, playerPos.Y, 0);

            Path path = null;
            try
            {
                path = goalMap.FindPathAvoidingGoals(pos.X, pos.Y);
            }
            catch(PathNotFoundException)
            {
                //(message in logger?)(Monster covers in fear)
                System.Console.WriteLine("RunAway - PathNotFoundException");
            }
            ac.Map.MapData.SetIsWalkable(pos.X, pos.Y, false);
            ac.Map.MapData.SetIsWalkable(playerPos.X, playerPos.Y, false);

            if(path != null)
            {
                try
                {
                    ac.Sprite.Move(path.StepForward());

                }
                catch(NoMoreStepsException)
                {
                    //(message in logger?)(Monster covers in fear)
                    System.Console.WriteLine("RunAway - NoMoreStepsException");
                }
            }
            return true;
        }
	}
}

