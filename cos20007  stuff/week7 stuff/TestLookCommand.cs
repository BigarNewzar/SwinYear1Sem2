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
    public class TestLookCommand

    {
        public Player player;
        public Item gem, unk;
        public Bag bag;
        public LookCommand look;

       

        [SetUp]
        public void Setup()
        {

            player = new Player("hero", "me");

            bag = new Bag(new string[] { "bag" }, "bag", "main bagpack");
            

            gem = new Item(new String[] { "gem" }, "a gem", "This is a good gem");

            unk = new Item(new String[] { "unk" }, "a unk", "This is a mysterious object called unk");

            look = new LookCommand();

            
        }




        [Test]
        public void TestLookAtMe()
        {
            Assert.AreEqual(player.FullDescription, look.Execute(player, new string[] { "look", "at", "inventory" }));

        }

       

        [Test]
        public void TestLookAtGem()
        {
            player.Inventory.Put(gem);

            Assert.AreEqual(gem.FullDescription, look.Execute(player, new string[] { "look", "at", "gem" }));
            //it should return "this is a good gem" for both sides!
        }

        [Test]
        public void TestLookAtUnk()
        {
            player.Inventory.Put(unk);

            Assert.AreEqual("I can't find the gem", look.Execute(player, new string[] { "look", "at", "gem" }));

        }

        [Test]
        public void TestLookAtGemInMe()
        {
            player.Inventory.Put(gem);

            Assert.AreEqual(gem.FullDescription, look.Execute(player, new string[] { "look", "at", "gem", "in", "inventory" }));

        }

        [Test]
        public void TestLookAtGemInBag()
        {
            bag.Inventory.Put(gem);
            player.Inventory.Put(bag);

            Assert.AreEqual(gem.FullDescription, look.Execute(player, new string[] { "look", "at", "gem", "in", "bag" }));
           

        }
        
        

        [Test]
        public void TestLookAtGemInNoBag()
        {
            bag.Inventory.Put(gem);


            Assert.AreEqual("I can't find the bag", look.Execute(player, new string[] { "look", "at", "gem", "in", "bag" }));

        }

        [Test]
        public void TestLookNoGemInBag()
        {
            player.Inventory.Put(bag);


            Assert.AreEqual("I can't find the gem", look.Execute(player, new string[] { "look", "at", "gem", "in", "bag" }));

        }

        [Test]
        public void TestInvalidLook()
        {
            
            Assert.AreEqual("I don't know how to look like that", look.Execute(player, new string[] { "look", "around"}));

            Assert.AreEqual("Error in look input", look.Execute(player, new string[] { "hello" }));

            Assert.AreEqual("I can't find the b", look.Execute(player, new string[] { "look", "at", "a", "in", "b" }));

            Assert.AreEqual("What do you want to look at?", look.Execute(player, new string[] { "look", "something", "a", "in", "b" }));

            Assert.AreEqual("What do you want to look in?", look.Execute(player, new string[] { "look", "at", "a", "something", "b" }));

        }
    }
}
