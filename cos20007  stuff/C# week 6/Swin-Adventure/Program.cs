using System;

namespace Swin_Adventure
{
    public class Program
    {
        static void Main(string[] args)
        {
            string name, desc;

            Console.WriteLine("What's your name?");
            name = Console.ReadLine();
            Console.WriteLine("Tell me something about yourself");
            desc = Console.ReadLine();

         
            Player player = new Player(name, desc);

            Item sword = new Item(new string[] { "sword" }, "sword", "an old rusty sword");

            Item torch = new Item(new string[] { "torch" }, "torch", "a torch to light the way");

            player.Inventory.Put(sword);

            player.Inventory.Put(torch);

                
            Bag bag = new Bag(new string[] { "bag" }, "bagpack", "initial bagpack for player");

            player.Inventory.Put(bag);

            

            Item orb = new Item(new string[] { "orb" }, "shiny orb", "a mysterious orb that seems to glow from within");

            bag.Inventory.Put(orb);


            Console.WriteLine("Do you want to see something or exit? \n To see anything, enter look commands \t \t To exit, type quit (case irrelivent)");

            //to ensure it can read it can break the text into an array for processing look command
            //reference: https://www.c-sharpcorner.com/UploadFile/mahesh/split-string-in-C-Sharp/

            string[] StringToSubstring(string text) 
            {
                char delimiterChar = ' ';// space will be used to seperate them
                return text.Split(delimiterChar);// split will split the string into substrings
            }



            string TextMain = Console.ReadLine();


            
            while (TextMain != "quit")
                {

                LookCommand look = new LookCommand();

                Console.WriteLine(look.Execute(player, StringToSubstring(TextMain)));
               
                TextMain = Console.ReadLine();

                }
            
            

        }
    }
}
