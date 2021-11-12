
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework; 
using Swin_Adventure;

namespace NunitTests
{
    [TestFixture]
    public class TestIdentifiableObject
    {
      
        //For Example:
        IdentifiableObject _id =
    new IdentifiableObject(new string[] { "ID1", "id2" });


        
        [SetUp]
        public void SetUp()
        {

        }

       
        [Test]
        public void TestAreYou()
        {
            //define expected result
            Boolean expected = true;

            //run the test
            _id.AreYou("id1");

            //define actual result
            Boolean actual = _id.AreYou("id1");

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestNotAreYou()
        {
            //define expected result
            Boolean expected = false;

            //run the test
            _id.AreYou("deadpool");

            //define actual result
            Boolean actual = _id.AreYou("deadpool");

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCaseSensitive()
        {
            //define expected result
            Boolean expected = true;

            //run the test
            _id.AreYou("iD1");//the data stored is id1 in lowercase. So if iD1 matches, then case doesn't matter can be proven!

            //define actual result
            Boolean actual = _id.AreYou("iD1");

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestFirstID()
        {
            //define expected result
            string expected = "id1";

            //run the test
            _id.FirstID.ToString();

            //define actual result
            string actual = _id.FirstID.ToString();

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAddID()
        {
            //the command to add "testcase" to the array
            _id.AddIdenfier("testcase");

            //define expected result for test if "testcase" was in the array
            Boolean expected = true;

            //run the test to check if "testcase" now exists in the array
            _id.AreYou("testcase");
            

            //define actual result
            Boolean actual = _id.AreYou("testcase");

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
