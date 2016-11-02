using System;
using SadConsole.Shapes;
using SadConsole.Consoles;
using Microsoft.Xna.Framework;


namespace ConsoleScreenGameHelper.Core.Console
{
	public class BorderedConsole : SadConsole.Consoles.Console
	{
        TextSurface borderSurface;
        SurfaceEditor borderEdit;
		public BorderedConsole(int width, int height) : base(width-1, height-1)
		{
            borderSurface = new TextSurface(width, height, base.textSurface.Font);
            borderEdit = new SurfaceEditor(borderSurface);
            Box box = Box.GetDefaultBox();
            box.Width = borderSurface.Width;
            box.Height = borderSurface.Height;
            box.Draw(borderEdit);
		}
        public BorderedConsole(string title, int width, int height) : this(width, height)
        {
            if (title.Length > width-2)
            {
                throw new ArgumentOutOfRangeException("Title wider then width");
            }
            borderEdit.Print(2, 0, title);
        }
        public override void Render()
        {
            //Correcting Position to have border shown if a "BorderedConsole"'s border is out of bounds
            base.Render();
            this.Position += new Point(1, 1);
            Renderer.Render(borderSurface, this.Position - new Point(1,1));
            this.Position -= new Point(1, 1);
        }
	}
}

