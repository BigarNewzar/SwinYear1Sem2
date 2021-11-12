using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounterTest;
using NUnit.Framework;


namespace TestCounterAndClock
{
    class CounterTestingStuff
    {
        Counter _counter = new Counter("tester");


        [SetUp]
        public void Setup()
        {
            _counter.Reset();
        }

        [Test]
        public void StartTime()
        {
            //define expected result
            int expected = 0;

            //run the test
            

            //define actual result
            int actual = _counter.Ticks;

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IncrementBy1()
        {
            //define expected result
            int expected = 1;

            //run the test
            _counter.Increment();

            //define actual result
            int actual = _counter.Ticks;

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void IncrementMultiple()
        {
            //define expected result
            int expected = 10;

            //run the test
            for(int i =0; i<10;i++)
            {
                _counter.Increment();
            }

            //define actual result
            int actual = _counter.Ticks;

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ResetTime()
        {
            //define expected result
            int expected = 0;

            //run the test
            for (int i = 0; i < 10; i++)
            {
                _counter.Increment();
            }
            _counter.Reset();

            //define actual result
            int actual = _counter.Ticks;

            // Assert expected and actual are the same


            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual);
        }
    }
}

