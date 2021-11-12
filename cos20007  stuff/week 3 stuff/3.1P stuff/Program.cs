using System;
using SplashKitSDK;
using System.Collections.Generic;
namespace ShapeDrawer
{
    public class Program
    {

        public static void Main()
        {
            Drawing myDrawing = new Drawing ();
            

            new Window("Shape Drawer", 800, 600);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                Shape myShape = new Shape();
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    
                    myDrawing.AddShape(myShape);
                    myShape.X = SplashKit.MouseX();
                    myShape.Y = SplashKit.MouseY();
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
