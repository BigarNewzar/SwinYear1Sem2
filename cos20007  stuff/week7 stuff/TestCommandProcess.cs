using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Swin_Adventure;

namespace NUnitTests
{
    [TestFixture]
    public class TestCommandProcess

    {
        public Player player;
        public Item gem;
        public Bag bag;
        public LookCommand look;

        public MoveCommand move;
        public Location smallcloset;
        public Path hallway_path;
        public Location hallway;
        public List<Path> pathlist;


        public CommandProcessor command;

        [SetUp]
        public void Setup()
        {

            player = new Player("hero", "me");
            bag = new Bag(new string[] { "bag" }, "bag", "main bagpack");
            gem = new Item(new String[] { "gem" }, "a gem", "This is a good gem");


            look = new LookCommand();

            move = new MoveCommand();
            smallcloset = new Location(new String[] { "small closet" }, "small closet",
                                                "A small dark closet, with an odd smell");

            hallway_path = new Path(new String[] { "South" }, smallcloset,
                                         "hallway_path", "You go through a door.");
            hallway = new Location(new String[] { "hallway", "long hallway" }, "hallway",
                                            "This is a long well lit hallway");
            player.Location = hallway;
            hallway.PutPath(hallway_path);
            hallway.GetPathIndividual(hallway_path.FirstID);

            command = new CommandProcessor(new string[] { }, new Command[] { look, move });
        }



        [Test]
        public void TestLookCommandAndCommandProcessor()
        {
            bag.Inventory.Put(gem);
            player.Inventory.Put(bag);

            
            Assert.AreEqual(look.Execute(player, new string[] { "look", "at", "gem", "in", "bag" }), command.Execute(player, new string[] { "look", "at", "gem", "in", "bag" }));

        }

        [Test]
        public void TestMoveCommandAndCommandProcessor()
        {
            




            string[] StringToSubstring(string text)
            {
                char delimiterChar = ' ';// space will be used to seperate them
                return text.Split(delimiterChar);// split will split the string into substrings
            }

            //command is giving the same output as that for move so it works for move as well!
            Assert.AreEqual("You head south\nYou go through a door.\nYou have arrived in a small closet", command.Execute(player, StringToSubstring("move south")));


        }








    }
}
