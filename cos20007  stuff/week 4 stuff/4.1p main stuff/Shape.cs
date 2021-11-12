using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ShapeDrawer
{
    public abstract class Shape
    {
        private Color _color;
        private float _x, _y;
        
        private Boolean _selected;

        public Shape() : this (Color.Yellow)
        {
        }

        public Shape(Color color) 
        {
            _color = color;
            

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

        public abstract void Draw();

        public abstract Boolean IsAt(Point2D pt);


        public abstract void DrawOutline();
        
    }
}
