using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class MoveInfo : Component
	{
        public static bool[,] CollisionMap { get; set; }
        public static EntityContainer Entities { get; set; }

        public override ComponentType ComponentType { get { return ComponentType.MoveInfo; } }

        public MoveInfo()
        {
        }

        public static bool IsWalkable(int x, int y)
        {
            return CollisionMap[x,y];
        }

        public override void FireEvent(object sender, EventArgs e)
        {

        }
	}
}

