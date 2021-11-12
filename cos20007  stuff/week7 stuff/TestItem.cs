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
    public class TestItem
    {
        public Item items;
        [SetUp]
        public void Setup() 
        { 
            items = new Item(new String[] { "shovel", "spade" }, "a shovel", "This is a might fine..."); 
        }

        [Test] 
        public void TestItemIsIdentifiable() 
        {
            Assert.AreEqual(true, items.AreYou("shovel")); 
        }

        [Test] 
        public void TestShortDesciption()
        { 
            Assert.AreEqual("a shovel (shovel)", items.ShortDescription); //no need tab or new line for this!
        }

        [Test] 
        public void TestFullDesciption() 
        { 
            Assert.AreEqual("This is a might fine...", items.FullDescription );
        }
    }
}
