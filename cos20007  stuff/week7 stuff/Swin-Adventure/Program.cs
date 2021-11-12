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

            


        Location smallcloset = new Location(new String[] { "small closet" }, "small closet",
                                                "A small dark closet, with an odd smell");

            Path hallway_path = new Path(new String[] { "South" }, smallcloset,
                                        "hallway_path", "You go through a door.");
            Location hallway = new Location(new String[] { "hallway", "long hallway" }, "hallway",
                                            "This is a long well lit hallway");

            player.Location = hallway;

            hallway.PutPath(hallway_path);
            //smallcloset.PutPath(hallway_path);
            hallway.GetPathIndividual(hallway_path.FirstID);//not sure if this is really needed




            Console.WriteLine("Do you want to see something or exit? \n To see anything, enter commands \t \t To exit, type quit (case irrelivent)");

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
                MoveCommand move = new MoveCommand();
               
                CommandProcessor command = new CommandProcessor(new string[] { }, new Command[] { look, move});
                

                Console.WriteLine(command.Execute(player, StringToSubstring(TextMain)));
               
                TextMain = Console.ReadLine();

                }
            
            

        }
    }
}
