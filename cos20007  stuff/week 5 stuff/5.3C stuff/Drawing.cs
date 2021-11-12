﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public class Drawing
    {
        private readonly List<Shape> _shapes;
        private Color _background;

        public Drawing() : this(Color.White)
        {
        }
        public Drawing(Color background)
        {
            _shapes = new List<Shape>();
            _background = background;
        }

        

        public List<Shape> SelectedShapes
        {
            get {
                List<Shape> result = new List<Shape>();

                foreach (Shape s in _shapes)
                {
                    if (s.selected)
                    {
                        result.Add(s);
                    }
                }
                return result;
            }
        }
        
        public int ShapeCount
        {
            get { return _shapes.Count; }
        }
        public Color Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
            }
        }

        public void Draw()
        {
            SplashKit.ClearScreen(_background);

            foreach (Shape s in _shapes)
            {
                s.Draw();
            }
        }

        public void SelectShapesAt(Point2D pt)
        {
            
            foreach(Shape s in _shapes)
            {
                if (s.IsAt(pt))
                {
                    s.selected = true;
                }
                else{
                    s.selected = false;
                }
            }
        }



        public void AddShape(Shape s)
        {
            _shapes.Add(s);
        }

        public void RemoveShape(Shape s)
        {

            _shapes.Remove(s);

        }  
        
        public void Save(string filename)
        {
            StreamWriter writer = new StreamWriter(filename);
            try
            {
                //Shape s ;

                writer.WriteColor(Background);
                writer.WriteLine(ShapeCount);

                foreach (Shape s in _shapes)
                {
                    s.SaveTo(writer);
                }
            }
            finally
            {
                writer.Close();
            }
        }

        public void Load(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            try
            {
                
                int count;
                Shape s;
                string kind;
                Background = reader.ReadColor();
                count = reader.ReadInteger();
                _shapes.Clear();

                for (int i = 0; i < count; i++)
                {
                    kind = reader.ReadLine();
                    s= Shape.CreateShape(kind);

                    s.LoadFrom(reader);
                   AddShape(s);
                }

            }
            finally
            {
                reader.Close();
            }


        }

    }
}