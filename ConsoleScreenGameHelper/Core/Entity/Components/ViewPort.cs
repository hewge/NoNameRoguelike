using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class ViewPort : Component
	{
        [JsonProperty]
        public int ViewWidth { get; set; }
        [JsonProperty]
        public int ViewHeight { get; set; }
        [JsonProperty]
        public int MapWidth { get; set; }
        [JsonProperty]
        public int MapHeight { get; set; }

        public override ComponentType ComponentType { get { return ComponentType.ViewPort; } }

		public ViewPort(int viewWidth, int viewHeight, int mapWidth, int mapHeight)
		{
            ViewWidth = viewWidth;
            ViewHeight = viewHeight;
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            this.RenderArea = new Rectangle(0, 0, viewWidth, viewHeight);
		}

        public override void FireEvent(object sender, EventArgs e)
        {

        }

	}
}

