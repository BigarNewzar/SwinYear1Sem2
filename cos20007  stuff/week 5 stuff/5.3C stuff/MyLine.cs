using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class MyLine: Shape
    {
        private float _endX;
        private float _endY;

        public MyLine(Color Clr, float startX, float startY, float endX, float endY)
        {

            Color = Clr;
            X = startX;
            Y = startY;
            _endX = endX;
            _endY = endY;


        }
        public MyLine() : this(Color.Red, 0, 0, 0, 0)
        {
        }

        public float EndX
        {
            get
            {
                return _endX;
            }
            set
            {
                _endX = value;
            }
        }

        public float EndY
        {
            get
            {
                return _endY;
            }
            set
            {
                _endY = value;
            }
        }
        public override void Draw()
        {
            SplashKit.DrawLine(Color, X, Y, EndX,EndY);

            if (selected)
            {
                DrawOutline();

            }
        }
        public override void DrawOutline()
        {
            SplashKit.DrawCircle(Color.Black, x: X, y: Y, 2
                );//cricle outline at start of line
            SplashKit.DrawCircle(Color.Black, x: EndX, y: EndY, 2
                );//cricle outline at end of line
            SplashKit.DrawRectangle(Color.Black, x: X + 2, y: Y + 2,(EndX-X) + 4, (EndY - Y) + 4
                ); //rectangle outline for the straight part
        }
        public override Boolean IsAt(Point2D pt)
        {
            if (SplashKit.PointOnLine(pt, SplashKit.LineFrom(X, Y, EndX,EndY)))
            {
                return true;

            }
            else
                return false;
        }

        public override void SaveTo(StreamWriter writer)
        {
            //writer.WriteLine("Line");
            base.SaveTo(writer);

            writer.WriteLine(EndX);
            writer.WriteLine(EndY);
        }
        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);

            EndX = reader.ReadInteger();
            EndY = reader.ReadInteger();

        }
    }
}
