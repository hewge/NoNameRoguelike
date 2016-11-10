using System;
using SadConsole.Input;
using Microsoft.Xna.Framework;
using SadConsole;

namespace ConsoleScreenGameHelper.Core.Console
{
	public class ScrollingConsole : SadConsole.Consoles.Console
	{
        SadConsole.Consoles.ControlsConsole controlsContainer;
        SadConsole.Controls.ScrollBar scrollBar;

        int scrollingCounter;

		public ScrollingConsole(int width, int height, int bufferHeight) : base(width -1, bufferHeight)
		{
            controlsContainer = new SadConsole.Consoles.ControlsConsole(1, height);
            textSurface.RenderArea = new Rectangle(0, 0, width, height);

            scrollBar = SadConsole.Controls.ScrollBar.Create(System.Windows.Controls.Orientation.Vertical, height);
            scrollBar.IsEnabled = false;
            scrollBar.ValueChanged += scrollBar_ValueChanged;

            controlsContainer.Add(scrollBar);
            controlsContainer.Position = new Point(Position.X + width - 1, Position.Y);
            controlsContainer.IsVisible = true;
            controlsContainer.MouseCanFocus = false;
            controlsContainer.ProcessMouseWithoutFocus = true;

            scrollingCounter = 0;

            //--- this ... ----
            VirtualCursor.Position = new Point(0, bufferHeight);
		}

        private void scrollBar_ValueChanged(object sender, EventArgs e)
        {
            textSurface.RenderArea = new Rectangle(0, scrollBar.Value, textSurface.Width, textSurface.RenderArea.Height);
        }

        public override void Render()
        {
            base.Render();
            controlsContainer.Render();
        }

        protected override void OnPositionChanged(Point oldLocation)
        {
            controlsContainer.Position = new Point(Position.X + Width, Position.Y);
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

            if(TimesShiftedUp != 0 || _virtualCursor.Position.Y >= textSurface.RenderArea.Height + scrollingCounter)
            {
                scrollBar.IsEnabled = true;
                if(scrollingCounter < textSurface.Height - textSurface.RenderArea.Height)
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

