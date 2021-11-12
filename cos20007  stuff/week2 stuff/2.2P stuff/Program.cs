using System;
using SplashKitSDK;
namespace ShapeDrawer
{
    public class Program
    {
        public static void Main()
        {
            Shape myShape = new Shape();

            new Window("Shape Drawer", 800, 600);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    myShape.X = SplashKit.MouseX();
                    myShape.Y = SplashKit.MouseY();
                }

                if (myShape.IsAt(SplashKit.MousePosition()) && SplashKit.KeyTyped(KeyCode.SpaceKey)) {
                    myShape.Color = SplashKit.RandomRGBColor(255);
                }

                myShape.Draw();




                SplashKit.RefreshScreen();
            } 
            while (!SplashKit.WindowCloseRequested("Shape Drawer"));
        }
    }
}
