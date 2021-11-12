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
    public class TestInventory
    {
        public Inventory inventory; 
        public Item shovel;

        [SetUp]
        public void Setup()
        {
            inventory = new Inventory();
            shovel = new Item(new String[] { "shovel", "spade" }, "a shovel", "This is a might fine ...");
            inventory.Put(shovel);      //currently inventory has shovel only
        }
        [Test] 
        public void TestFindItem() 
        {
            Assert.AreEqual(true, inventory.HasItem("shovel")); 
        } 
        [Test] 
        public void TestNoItemFound() 
        {
            
            Assert.AreEqual(false, inventory.HasItem("deadpool")); 
        } 
        [Test] public void TestFetchItem() 
        {

            Assert.AreEqual(shovel, inventory.Fetch("shovel")); 
            //bring out but not remove from inventory

            Assert.AreEqual(true, inventory.HasItem("shovel")); 
        } 
        [Test] public void TestTakeItem() 
        {
            Assert.AreEqual(shovel, inventory.Take("shovel"));
            //removed from inventory

            Assert.AreEqual(false, inventory.HasItem("shovel")); 
        }
        [Test]
        public void TestItemList()
        {
            Assert.AreEqual("\ta shovel (shovel)\n", inventory.ItemList);
        }
        
    }
}

