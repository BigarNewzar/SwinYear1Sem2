using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public abstract class Shape
    {

        private static Dictionary<string, Type> _ShapeClassRegistry = new Dictionary<string, Type>();

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

        public static void RegisterShape(string name, Type t)
        {
            _ShapeClassRegistry[name] = t;
        }

        //output string key, input Type value
        public static string GetKeyOfType(Type t)
        {//look through all the keys in the dictionary
           foreach (string key in _ShapeClassRegistry.Keys)
            {//if type matches then output the key string
                // else output no key found
                //getting value of dictionary using key


                //You can access individual key / value pair of the Dictionary by using its index value.Here, you just specify the key in the index to get the value from the given dictionary

                //link: https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/


                if (t== _ShapeClassRegistry[key])
                {
                    return key;
                }
            }
            return "No Key Found for the type";
            
            
        }

        public static Shape CreateShape(string name)
        {
            return (Shape)Activator.CreateInstance(_ShapeClassRegistry[name]);
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
        public virtual void SaveTo( StreamWriter writer)
        {   //how to call the type for all the child classes?

            //shouldnt the child follow the the command in the virtual method
            //then see "this" keyword and thus put its own shape type (like rectangle) and thus
            //provide the key?

            writer.WriteLine(GetKeyOfType(this.GetType()));

            writer.WriteColor(_color);
            writer.WriteLine(X);
            writer.WriteLine(Y);
        }

        public virtual void LoadFrom(StreamReader reader)
        {
            Color = reader.ReadColor();
            X = reader.ReadInteger();
            Y = reader.ReadInteger();
            
        }

    }
}
