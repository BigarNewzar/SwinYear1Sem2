using System;

namespace TestLibrary
{
    class Program
    {
        static void Main(string[] args)
        {   //step 1:
            Library library = new Library("DeadPool's Library");

            //step 2:
            Book XForce = new Book("XForce", "Colossus", "BN565");

            Book GreenLanternMistake = new Book("Green Lantern was a mistake", "Deadpool", "BN233");

           
            Game ShootEmDead = new Game("Shoot Em Dead", "Deadpool", "PG");

            Game MineCraft = new Game("MineCraft", "Mojang", "PG-13");


            library.AddResource(XForce);
            library.AddResource(GreenLanternMistake);
            library.AddResource(ShootEmDead);
            library.AddResource(MineCraft);

            XForce.OnLoan = true;
            ShootEmDead.OnLoan = true;


            //step 3:
            //it will print true as not on loan
            Console.WriteLine(library.HasResource("Green Lantern was a mistake"));

            //step 4:
            //it will print false as on loan
            Console.WriteLine(library.HasResource("Shoot Em Dead"));

        }
    }
}
