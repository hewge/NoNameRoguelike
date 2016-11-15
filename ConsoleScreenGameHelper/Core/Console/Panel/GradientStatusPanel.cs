using System;
using ConsoleScreenGameHelper.Core.DataContainer;
using ConsoleScreenGameHelper.Interface;
using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Consoles;

namespace ConsoleScreenGameHelper.Core.Console.Panel
{
	public class GradientStatusPanel : TextSurface, IStatusPanel
	{
        SurfaceEditor editor;
        public BaseStat stat;
        Color colorOne;
        Color colorTwo;

		public GradientStatusPanel(BaseStat bs, int width, int height, Color colorOne, Color colorTwo) : base(width, height)
		{
            this.colorOne = colorOne;
            this.colorTwo = colorTwo;
            editor = new SurfaceEditor(this);
            stat = bs;
		}

        public void Update()
        {
            editor.Clear();
            editor.Print(0, 0, stat.Name);
            //calculation move to method?
            ColoredString statusString = stat.Value.ToString().CreateColored(colorOne, Color.Black, null)+
                "/".CreateColored(Color.White, Color.Black, null)+
                stat.AltValue.ToString().CreateColored(colorTwo, Color.Black, null);
            ColoredString gradient = new string((char)176, 14).CreateGradient(colorOne, colorTwo, colorOne, colorTwo);
            double percent = (double)stat.Value/(double)stat.AltValue;

            //drawing
            editor.Print(width - statusString.ToString().Length, 0, statusString);
            editor.Print(0, 1, gradient.SubString(0, (int)((double)gradient.Count*(double)percent)));
        }
	}
}

