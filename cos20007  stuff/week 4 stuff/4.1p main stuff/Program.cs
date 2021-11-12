using System;
using SplashKitSDK;
using System.Collections.Generic;
namespace ShapeDrawer
{
    public class Program
    {
        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line
        }
        public static void Main()
        {
            ShapeKind kindToAdd = new ShapeKind();
            kindToAdd = ShapeKind.Circle;
            

            Drawing myDrawing = new Drawing ();
            

            new Window("Shape Drawer", 800, 600);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    kindToAdd = ShapeKind.Rectangle;
                }

                if (SplashKit.KeyTyped(KeyCode.CKey))
                {
                    kindToAdd = ShapeKind.Circle;
                }
                if (SplashKit.KeyTyped(KeyCode.LKey))
                {
                    kindToAdd = ShapeKind.Line;
                }


                Shape myShape = new MyRectangle();
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {

                    Shape newShape;
                    if(kindToAdd == ShapeKind.Circle)
                    {
                        MyCircle newCircle = new MyCircle();
                        newShape = newCircle;

                    }
                    else if (kindToAdd == ShapeKind.Line)
                        {
                            MyLine newLine = new MyLine();
                            newShape = newLine;

                        }
                        else
                    {
                        MyRectangle newRect = new MyRectangle();
                        newShape = newRect;

                    }

                    newShape.X = SplashKit.MouseX();
                    newShape.Y = SplashKit.MouseY();


                    myDrawing.AddShape(newShape);


                    //myDrawing.AddShape(myShape);
                    //myShape.X = SplashKit.MouseX();
                    //myShape.Y = SplashKit.MouseY();
                }

                   
            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    myDrawing.Background = SplashKit.RandomRGBColor(255); 

                }


                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    myDrawing.SelectShapesAt(SplashKit.MousePosition());
              
                }
               
                if (SplashKit.KeyTyped(KeyCode.BackspaceKey) || SplashKit.KeyTyped(KeyCode.DeleteKey))

                    
                {   foreach(Shape s in myDrawing.SelectedShapes)
                    myDrawing.RemoveShape(s);

                }



                myDrawing.Draw();
                
                SplashKit.RefreshScreen();

            } 



            while (!SplashKit.WindowCloseRequested("Shape Drawer"));
        }
    }
}
