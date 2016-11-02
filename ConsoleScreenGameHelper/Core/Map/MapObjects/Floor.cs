using System;
using SadConsole;
using Microsoft.Xna.Framework;
using ConsoleScreenGameHelper;

namespace ConsoleScreenGameHelper.Core.Map.MapObjects
{
	public class Floor : MapObjectBase
	{
		public Floor() : base(Swatch.DbDarkGray, Color.Transparent, 46)
		{
		}
	}
}

