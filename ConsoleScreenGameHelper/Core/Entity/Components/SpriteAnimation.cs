using System;
using Newtonsoft.Json;
using RogueSharp;
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

        private void AlignViewPort()
        {
            var a = GetComponent<Actor>(ComponentType.Actor);
            if(a != null)
            {
                var RenderArea = a.Map.MapData.textSurface.RenderArea;
                var area = new Rectangle(this.Position.X - (RenderArea.Width / 2),
                        this.Position.Y - (RenderArea.Height / 2),
                        RenderArea.Width, RenderArea.Height);
                if (area.Width > a.Map.Width)
                    area.Width = a.Map.Width;
                if (area.Height > a.Map.Height)
                    area.Height = a.Map.Height;

                if (area.X < 0)
                    area.X = 0;
                if (area.Y < 0)
                    area.Y = 0;

                if (area.X + area.Width > a.Map.Width)
                    area.X = a.Map.Width- area.Width;
                if (area.Y + area.Height > a.Map.Height)
                    area.Y = a.Map.Height- area.Height;

                if(a != null)
                {

                    a.Map.MapData.textSurface.RenderArea = area;
                }

                Offset = new Point(0, 0) - area.Location;



            }
        }
        protected override void OnPositionChanged(Point oldLocation)
        {
            var a = GetComponent<Actor>(ComponentType.Actor);
            if(a != null)
            {
                if(a.Map != null)
                {
                    if(a.Map.CameraFollow == GetParent())
                    {

                        AlignViewPort();
                    }

                    a.Map.MapData.Map.SetCellProperties(oldLocation.X, oldLocation.Y, true, true);
                    a.Map.MapData.Map.SetCellProperties(this.Position.X, this.Position.Y, true, false);
                }
            }
        }
        public void Move(ICell cell)
        {
            Point mv = new Point(0, 0);
            if(cell.X > Position.X)
                mv += new Point(1, 0);
            if(cell.X < Position.X)
                mv += new Point(-1, 0);
            if(cell.Y < Position.Y)
                mv += new Point(0, -1);
            if(cell.Y > Position.Y)
                mv += new Point(0, 1);

            Move(mv);

        }
        private void Move(Point amount)
        {
            Point newPos = this.Position + amount;
            //System.Console.WriteLine(string.Format("Try Move to X:{0}, Y:{1}", newPos.X, newPos.Y));
            var a = GetComponent<Actor>(ComponentType.Actor);
            //System.Console.WriteLine(string.Format("Is X:{0}, Y:{1}, Walkable?:{2}", newPos.X, newPos.Y, a.Map.MapData.IsWalkable(newPos.X, newPos.Y)));
            if(a.Map.MapData.IsWalkable(newPos.X, newPos.Y))
            {

                this.Position += amount;
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

