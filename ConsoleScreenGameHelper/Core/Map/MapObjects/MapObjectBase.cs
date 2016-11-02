using System;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Effects;
using ConsoleScreenGameHelper;

namespace ConsoleScreenGameHelper.Core.Map.MapObjects
{
	public abstract class MapObjectBase
	{
        public CellAppearance Appearance { get; set; }
        public ICellEffect EffectSeen { get; set; }
        public ICellEffect EffectHidden { get; set; }

		public MapObjectBase(Color foreground, Color background, int character)
		{
            Appearance = new CellAppearance(foreground, background, character);
            EffectSeen = new Recolor()
            {
                Foreground = Color.LightGray * 0.3f,
                Background = Color.LightGray * 0.3f,
                DoForeground = true,
                DoBackground = true,
                CloneOnApply = false
            };

            EffectHidden = new Recolor()
            {
                Foreground = Color.Black,
                Background = Color.Black,
                DoForeground = true,
                DoBackground = true,
                CloneOnApply = false
            };
		}

        public virtual void RenderToCell(SadConsole.Cell sadConsoleCell, bool isFov, bool isExplored)
        {
            Appearance.CopyAppearanceTo(sadConsoleCell);

            if (sadConsoleCell.Effect != null)
            {
                sadConsoleCell.Effect.Clear(sadConsoleCell);
                sadConsoleCell.Effect = null;
            }

            if (isFov)
            {
            }
            else if (isExplored)
            {
                sadConsoleCell.Effect = EffectSeen;
                sadConsoleCell.Effect.Apply(sadConsoleCell);
            }
            else
            {
                sadConsoleCell.Effect = EffectHidden;
                sadConsoleCell.Effect.Apply(sadConsoleCell);
            }
        }

        public virtual void RemoveCellFromView(SadConsole.Cell sadConsoleCell)
        {
            if (sadConsoleCell.Effect != null)
            {
                sadConsoleCell.Effect.Clear(sadConsoleCell);
                sadConsoleCell.Effect = null;
            }

            sadConsoleCell.Effect = EffectSeen;
            sadConsoleCell.Effect.Apply(sadConsoleCell);
        }
	}
}

