using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Swin_Adventure;

namespace NUnitTests
{
	[TestFixture()]
	public class TestPath
	{
		public Location smallcloset;
		public Path hallway_path;
		public Location hallway;
		public Player player;
		public List<Path> pathlist;
		public MoveCommand move;


		[SetUp]
        public void Setup()
        {
			smallcloset = new Location(new String[] { "small closet" }, "small closet",
												"A small dark closet, with an odd smell");
			
			hallway_path = new Path(new String[] {"South"}, smallcloset,
										"hallway_path", "You go through a door.");
			hallway = new Location(new String[] { "hallway", "long hallway" }, "hallway",
											"This is a long well lit hallway");

			player = new Player("me", "mighty programmer");
			move = new MoveCommand();

			//since using property to set those stuff instead of changing the contructor for the player or location,
			//must assign those properties from the start.
			//or else program will scream : NULL REFERENCE ERROR!
			player.Location = hallway;

			hallway.PutPath(hallway_path);
			hallway.GetPathIndividual(hallway_path.FirstID);


		}
		[Test()]
		public void TestMovePlayerToDestination()
		{
					
			hallway_path.Move(player);
			Assert.AreEqual(smallcloset, player.Location);
		}

		[Test()]
		public void TestGetPathFromLocation()
		{
			Assert.IsInstanceOf<Path> (hallway.GetPathIndividual("South"));
		}

		//Assert.IsInstanceOf<Path>(pathlist.Find(hallway_path));
		

		[Test()]
		public void TestValidPathIdentifier()
		{
		//adding hallway and smallcloset to the list of location
		


			
		

			string[] StringToSubstring(string text)
			{
				char delimiterChar = ' ';// space will be used to seperate them
				return text.Split(delimiterChar);// split will split the string into substrings
			}


			Assert.AreEqual("You head south\nYou go through a door.\nYou have arrived in a small closet", move.Execute(player, StringToSubstring("move south")));
		}

		[Test()]
		public void InvalidPathIdentifier()
		{

			string[] StringToSubstring(string text)
			{
				char delimiterChar = ' ';// space will be used to seperate them
				return text.Split(delimiterChar);// split will split the string into substrings
			}
			
			

			Assert.AreEqual("I don't know how to move like that", move.Execute(player, StringToSubstring("mov downwards")));
		}
	}
}
