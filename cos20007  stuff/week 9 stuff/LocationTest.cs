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
    public class LocationUnitTest
    {
        public Location location;
        public Player player;
        public Item infinity_stones;

        [SetUp]
        public void Setup()
        {
            infinity_stones = new Item(new String[] { "infinity_stones" }, "a infinity_stones", "Its a infinity_stones");
            location = new Location(new String[] { "valhara" }, "valhara", "Where the brave shall live forever");
            player = new Player("hero", "me");

            location.Inventory.Put(infinity_stones);
        }
        [Test()]
        public void TestLocateSelf()
        {

            Assert.AreEqual("valhara", location.Locate("valhara").FirstID);//both will return string "valhara"
        }

        [Test()]
        public void TestLocationLocateItems()
        {
            Assert.AreEqual(infinity_stones, location.Locate("infinity_stones"));
        }

        [Test()]
        public void TestPlayerLocateItemInLocation()
        {
            player.Location = location;//utilising setter to fix the location in player and avoid null error thingy
            Assert.AreEqual(infinity_stones, player.Locate("infinity_stones"));
        }

        
    }
}
