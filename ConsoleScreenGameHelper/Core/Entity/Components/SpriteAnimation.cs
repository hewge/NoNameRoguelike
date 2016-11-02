using System;
using Newtonsoft.Json;
using ConsoleScreenGameHelper.Enum;
using ConsoleScreenGameHelper.EventHandler;
using Microsoft.Xna.Framework;

namespace ConsoleScreenGameHelper.Core.Entity.Components
{
	public class SpriteAnimation : Component
	{
        public override ComponentType ComponentType { get { return ComponentType.SpriteAnimation; } }

        public static Point Offset { get; set; }
        [JsonProperty]
        public int Symbol { get; set;}
        [JsonProperty]
        public Color Foreground { get; set; }
        [JsonProperty]
        public Color Background { get; set; }

		public SpriteAnimation(int symbol, Color foregroundColor, Color backgroundColor)
		{
            Symbol = symbol;
            Foreground = foregroundColor;
            Background = backgroundColor;
            SetSingleFrameSymbol(symbol, foregroundColor, backgroundColor);
		}

        private void SetSingleFrameSymbol(int symbol, Color foregroundColor, Color backgroundColor)
        {
            this.IsVisible = true;
            this.Animation.CreateFrame();
            this.Animation.CurrentFrame[0].Foreground = foregroundColor;
            this.Animation.CurrentFrame[0].Background = backgroundColor;
            this.Animation.CurrentFrame[0].GlyphIndex = symbol;
            this.RepositionRects = true;
        }

        public override void Update()
        {
            this.RenderOffset = Offset;
            base.Update();
        }

        public override void FireEvent(object sender, EventArgs e)
        {
            if(e.GetType() == typeof(NewMoveEventArgs))
            {
                switch((e as NewMoveEventArgs).Direction)
                {
                    case Direction.Up:
                        Move(new Point(0, -1));
                        break;
                    case Direction.Down:
                        Move(new Point(0, 1));
                        break;
                    case Direction.Left:
                        Move(new Point(-1, 0));
                        break;
                    case Direction.Right:
                        Move(new Point(1, 0));
                        break;
                }
            }
        }

        private void AlignViewPort(ViewPort vp)
        {
                var area = new Rectangle(this.Position.X - (vp.RenderArea.Width / 2),
                        this.Position.Y - (vp.RenderArea.Height / 2),
                        vp.RenderArea.Width, vp.RenderArea.Height);
                if (area.Width > vp.MapWidth)
                    area.Width = vp.MapWidth;
                if (area.Height > vp.MapHeight)
                    area.Height = vp.MapHeight;

                if (area.X < 0)
                    area.X = 0;
                if (area.Y < 0)
                    area.Y = 0;

                if (area.X + area.Width > vp.MapWidth)
                    area.X = vp.MapWidth- area.Width;
                if (area.Y + area.Height > vp.MapHeight)
                    area.Y = vp.MapHeight- area.Height;

                vp.RenderArea = area;

                Offset = vp.Position - vp.RenderArea.Location;


        }
        protected override void OnPositionChanged(Point oldLocation)
        {
            //TODO: Kolla om enna entity är densamme som "ViewPort(den statiska kamera klassen/funktionen?) / Kamera" skall följa? det vill säga högst up på stacken.

			var ViewPort = GetComponent<ViewPort>(ComponentType.ViewPort);
            if(ViewPort != null)
            {
                AlignViewPort(ViewPort);
            }
            //TODO: Gör detta med FOV på ett helt annat sätt.. kanske kameran skall ha ansvar för detta? eller ta reda på om LineOfSight behövs/kan användas , i sådana fall kan dem ju vara i samma klass
            var FOV = GetComponent<FOV>(ComponentType.FOV);
            if(FOV != null)
            {
                FOV.Dirty = true;
            }

            var a = GetComponent<Actor>(ComponentType.Actor);
            if(a != null)
            {
                if(a.Map != null)
                {
                    a.Map.MapData.Map.SetCellProperties(oldLocation.X, oldLocation.Y, true, true);
                    a.Map.MapData.Map.SetCellProperties(this.Position.X, this.Position.Y, true, false);
                }
            }
        }
        private void Move(Point amount)
        {
            Point newPos = this.Position + amount;
            System.Console.WriteLine(string.Format("Try Move to X:{0}, Y:{1}", newPos.X, newPos.Y));
            var a = GetComponent<Actor>(ComponentType.Actor);
            System.Console.WriteLine(string.Format("Is X:{0}, Y:{1}, Walkable?:{2}", newPos.X, newPos.Y, a.Map.MapData.IsWalkable(newPos.X, newPos.Y)));
            if(a.Map.MapData.IsWalkable(newPos.X, newPos.Y))
            {

                this.Position += amount;
                System.Console.WriteLine(this.Position);
                //FIXME: If Entity Moves Change or set and remove IsWalkable or respective either here or in OnPositionChanged
                return;
	        }
            var atk = GetComponent<Attack>(ComponentType.Attack);
            if(atk != null)
            {
                System.Console.WriteLine("Attack");
                GetParent().FireEvent(this, new NewAttackEventArgs(newPos));
            }

        }
    }
}

