using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public class MyCircle: Shape
    {
        private int _radius;
        private Color _color;

        public MyCircle(Color Color, int radius)
        {
            
            _color = Color;
            _radius = radius;

        }
        public MyCircle() : this(Color.Blue, 50)
        {
        }

        public int Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
            }
        }
        public override void Draw()
        {
            SplashKit.FillCircle(Color, X, Y,
            _radius);

            if (selected)
            {
                DrawOutline();

            }
        }
        public override void DrawOutline()
        {
            SplashKit.DrawCircle(Color.Black, x: X, y: Y, _radius +2
                );
        }
        public override Boolean IsAt(Point2D pt)
        {
            
            if (SplashKit.PointInCircle(pt, SplashKit.CircleAt(X,Y, Radius)))
                
            {
                return true;

            }
            else
                return false;
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Circle");
            base.SaveTo(writer);

            writer.WriteLine(Radius);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);

            Radius = reader.ReadInteger();
            

        }
    }
}
