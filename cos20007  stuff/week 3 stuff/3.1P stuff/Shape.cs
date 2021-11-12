using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class Shape
    {
        private Color _color;
        private float _x, _y;
        private int _width, _height;
        private Boolean _selected;

        public Shape()
        {
            _color = Color.Green;
            _x = 0;
            _y = 0;
            _width = 100;
            _height = 100;
        }

        public Boolean selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
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

        public void Draw()
        {
            SplashKit.FillRectangle(_color, _x, _y,
            _width, _height);

            if (selected) { 
                DrawOutline(); 
            
            }
        }
        public Boolean IsAt( Point2D pt)
        {
            if (pt.X > _x && pt.X < (_width + _x) && pt.Y > _y && pt.Y < (_height + _y))
            {
                return true;

            }
            else
                return false;
        }

        public void DrawOutline()
        {
            SplashKit.DrawRectangle(Color.Black, x: _x - 2, y: _y - 2, width: _width + 4, height: _height + 4
                );
        }
    }
}
