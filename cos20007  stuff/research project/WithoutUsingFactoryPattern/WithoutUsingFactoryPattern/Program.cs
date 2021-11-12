using System;

namespace WithoutUsingFactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //instantiating new objects
            Item gun = new Gun("A45", "revolver", "this is a good revolver");
            Item sheild = new Sheild("B23", "Electric Sheild", "this is an electric shield") ;

            //outputing details about the object made
            Console.WriteLine(gun.itemDetails());
            Console.WriteLine(sheild.itemDetails());
            GC.Collect();
        }
    }
}
