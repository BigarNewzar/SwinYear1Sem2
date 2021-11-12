using System;

namespace UsingFactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //instantiating new factory
            ItemFactory itemFactory = new ItemFactory();
            
            //Letting the factory create the two types of items
            IItem gun = itemFactory.createItem("Gun");
            IItem sheild = itemFactory.createItem("Sheild");

            //outputing details about the object made
            Console.WriteLine(gun.itemDetails());
            Console.WriteLine(sheild.itemDetails());
            GC.Collect();
        }
    }
}
