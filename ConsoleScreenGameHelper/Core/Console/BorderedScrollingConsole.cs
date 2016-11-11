using System;
using SadConsole.Input;
using SadConsole.Shapes;
using SadConsole.Consoles;
using Microsoft.Xna.Framework;

namespace ConsoleScreenGameHelper.Core.Console
{
	public class BorderedScrollingConsole : SadConsole.Consoles.Console
	{
        TextSurface borderSurface;
        SurfaceEditor borderEdit;
        SadConsole.Consoles.ControlsConsole controlsContainer;
        SadConsole.Controls.ScrollBar scrollBar;

        int scrollingCounter;

		public BorderedScrollingConsole(int width, int height, int bufferHeight) : base(width -2, bufferHeight)
		{
            controlsContainer =  new SadConsole.Consoles.ControlsConsole(1, height-2);
            textSurface.RenderArea = new Rectangle(1, 1, width, height-2);
            scrollBar = SadConsole.Controls.ScrollBar.Create(System.Windows.Controls.Orientation.Vertical, height-2);
            scrollBar.IsEnabled = false;
            scrollBar.ValueChanged += scrollBar_ValueChanged;
            controlsContainer.Add(scrollBar);
            controlsContainer.Position = new Point(Position.X + width, Position.Y);
            controlsContainer.IsVisible = true;
            controlsContainer.MouseCanFocus = false;
            controlsContainer.ProcessMouseWithoutFocus = true;
            scrollingCounter = 0;
            borderSurface = new TextSurface(width, height, base.textSurface.Font);
            borderEdit = new SurfaceEditor(borderSurface);
            Box box = Box.GetDefaultBox();
            box.Width = width;
            box.Height = height;
            box.Draw(borderEdit);
		}

        private void scrollBar_ValueChanged(object sender, EventArgs e)
        {
            textSurface.RenderArea = new Rectangle(0, scrollBar.Value, textSurface.Width, textSurface.RenderArea.Height);
        }

        public BorderedScrollingConsole(string title, int width, int height, int bufferHeight) : this(width, height, bufferHeight)
        {
            if(title.Length > width - 3)
            {
                throw new ArgumentOutOfRangeException("Title wider than width.");
            }
            borderEdit.Print(2, 0, title);
        }

        public override void Render()
        {

            this.Position += new Point(1,1);
            base.Render();
            Renderer.Render(borderSurface, this.Position - new Point(1,1));

            this.Position -= new Point(1,1);

            controlsContainer.Render();
        }
        protected override void OnPositionChanged(Point oldValue)
        {
            controlsContainer.Position = new Point(Position.X + Width+1, Position.Y+1);
        }

        protected override void OnVisibleChanged()
        {
            controlsContainer.IsVisible = this.IsVisible;
        }

        public override bool ProcessMouse(MouseInfo info)
        {
            if(!controlsContainer.ProcessMouse(info))
            {
                return base.ProcessMouse(info);
            }
            return true;
        }
        public override void Update()
        {
            base.Update();
            controlsContainer.Update();
            if(TimesShiftedUp != 0 || _virtualCursor.Position.Y >= textSurface.RenderArea.Height + scrollingCounter + 1)
            {
                scrollBar.IsEnabled = true;
                if(scrollingCounter < textSurface.Height - textSurface.RenderArea.Height - 1)
                {
                    scrollingCounter += TimesShiftedUp != 0 ? TimesShiftedUp : 1;
                }
                scrollBar.Maximum = (textSurface.Height + scrollingCounter) - textSurface.Height;
                scrollBar.Value = scrollingCounter;
                TimesShiftedUp = 0;
            }
        }
	}
}

