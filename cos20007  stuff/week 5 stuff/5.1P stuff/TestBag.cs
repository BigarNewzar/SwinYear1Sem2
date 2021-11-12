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

    public class TestBag
    {

        public Bag bag1, bag2;
        public Item shovel, book, infinity_stones;

        [SetUp]
        public void Setup()
        {

            bag1 = new Bag(new string[] { "b1" }, "b1", "bagpack no.1");
            bag2 = new Bag(new string[] { "b2" }, "b2", "bagpack no.2");

            shovel = new Item(new String[] { "shovel" }, "a shovel", "This is a might fine ...");
            book = new Item(new String[] { "book" }, "a book", "Its a book");
            infinity_stones = new Item(new String[] { "infinity_stones" }, "a infinity_stones", "Its a infinity_stones");

            bag1.Inventory.Put(shovel);
            bag1.Inventory.Put(book);


            bag2.Inventory.Put(infinity_stones);
            bag1.Inventory.Put(bag2);

        }

        [Test]
        public void TestBagLocatesItem()
        {
            Assert.AreEqual(shovel, bag1.Locate("shovel"));
        }

        [Test]
        public void TestBagLocatesItself()
        {
            Assert.AreEqual(bag1, bag1.Locate("b1"));
        }

        [Test]
        public void TestBagLocatesNothing()
        {
            Assert.AreEqual(null, bag1.Locate("deadpool"));
        }

        [Test]
        public void TestBagFullDescription()
        {
            Assert.AreEqual("In the" + bag1 + "you can see: " + bag1.Inventory.ItemList, bag1.FullDescription);
        }

        [Test]
        public void TestBaginBag()
        {
            Assert.AreEqual(bag2, bag1.Locate("b2"));

            Assert.AreEqual(book, bag1.Locate("book"));

            //infinity stones are stored in bag2 so bag1 shouldnt access it
            Assert.AreEqual(null, bag1.Locate("infinity_stones"));

        }
    }

}

