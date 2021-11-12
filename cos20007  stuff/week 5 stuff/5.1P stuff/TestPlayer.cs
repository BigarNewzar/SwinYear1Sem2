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
    public class TestPlayer
    {
        public Inventory inventory;
        public Item shovel;
        public Player player;

        [SetUp]
        public void Setup()
        {
            inventory = new Inventory();
            shovel = new Item(new String[] { "shovel", "spade" }, "a shovel", "This is a might fine ...");
            player = new Player("hero", "me");
        }

        [Test]
        public void TestPlayerIsIdentifiable()
        {
            //define expected result
            Boolean expected = true;

            //run the test
            player.AreYou("me");

            //define actual result
            Boolean actual = player.AreYou("me");

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void TestPlayerLocatesItems()
        {
            //define expected result
            string expected = "\ta shovel (shovel)\n";

            //run the test
            inventory.Put(shovel);
            player.Locate("shovel");

            //define actual result
            string actual = inventory.ItemList;

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);

            Assert.AreEqual(true, inventory.HasItem("shovel"));  //No need to write the 4 lines for assert equal!just direct place the expected and actual parts in their place! Make sure to keep their data types same!
        }
        [Test]
        public void TestPlayerLocatesItself()
        {
            Assert.AreEqual(player.Locate("me"), player); 
        }
        [Test] 
        public void TestPlayerLocatesNothing() 
        {
            Assert.AreEqual(null, player.Locate("deadpool"));
            //checks if null is returned or not
        }
        [Test]
        public void TestPlayerFullDescription()
        {
            inventory.Put(shovel);

            StringAssert.Contains("You are carrying:" + player.Inventory.ItemList, player.FullDescription);



        }

    }
}