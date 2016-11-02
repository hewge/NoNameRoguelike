using System;
using SadConsole;
using Microsoft.Xna.Framework;
using ConsoleScreenGameHelper;

namespace ConsoleScreenGameHelper.Core.Map.MapObjects
{
	public class Wall : MapObjectBase
	{
		public Wall() : base(Swatch.DbBrightBrownWood, Swatch.DbLightGray, 176)
		{
		}
	}
}

