using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class MyRectangle : Shape
    {
        private int _width;
        private int _height;

        public MyRectangle(Color clr, float x, float y , int width, int height): base(clr)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

        }

        public MyRectangle(): this (Color.Green, 0, 0 , 100, 100)
        {
        }

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }
        public override void Draw()
        {
            SplashKit.FillRectangle(Color, X, Y,
            Width, Height);

            if (selected)
            {
                DrawOutline();

            }
        }
        public override void DrawOutline()
        {
            SplashKit.DrawRectangle(Color.Black, x: X-2, y: Y-2, Width + 4, Height + 4
                );
        }

        public override Boolean IsAt(Point2D pt)
        {
            if (pt.X > X && pt.X < (Width + X) && pt.Y > Y && pt.Y < (Height + Y))
            {
                return true;

            }
            else
            return false;
        }
    }
}
