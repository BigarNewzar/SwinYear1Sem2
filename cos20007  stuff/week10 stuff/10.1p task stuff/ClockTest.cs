using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CounterTest;


namespace TestCounterAndClock
{
    public class Tests
    {
        [TestFixture]

        public class ClockTest
        {
            Clock _clock = new Clock();


            [SetUp]
            public void Setup()
            {
                _clock.reset();
            }

            [Test]
            public void StartTime()
            {
                //define expected result
                string expected = "00:00:00";

                //run the test
                _clock.PrintTime();

                //define actual result
                String actual = _clock.PrintTime();

                // Assert expected and actual are the same


                //Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void TimeAfter30sec()
            {
                //define expected result
                string expected = "00:00:30";

                //run the test
                for(int i=0; i < 30; i++)
                {
                    _clock.tick();
                }
                    _clock.PrintTime();

                //define actual result
                String actual = _clock.PrintTime();

                // Assert expected and actual are the same


                //Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void TimeAfter60sec()
            {
                //define expected result
                string expected = "00:01:00";

                //run the test
                for (int i = 0; i < 60; i++)
                {
                    _clock.tick();
                }
                _clock.PrintTime();

                //define actual result
                String actual = _clock.PrintTime();

                // Assert expected and actual are the same


                //Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected, actual);
            }
            [Test]
            public void TimeAfter30min()
            {
                //define expected result
                string expected = "00:30:00";

                //run the test
                for (int i = 0; i < 30*60; i++)
                {
                    _clock.tick();
                }
                _clock.PrintTime();

                //define actual result
                String actual = _clock.PrintTime();

                // Assert expected and actual are the same


                //Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void TimeAfter1hr()
            {
                //define expected result
                string expected = "01:00:00";

                //run the test
                for (int i = 0; i < 3600; i++)
                {
                    _clock.tick();
                }
                _clock.PrintTime();

                //define actual result
                String actual = _clock.PrintTime();

                // Assert expected and actual are the same


                //Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected, actual);
            }
            [Test]
            public void TimeAt24hrIs0()
            {
                //define expected result
                string expected = "00:00:00";

                //run the test
                for (int i = 0; i < 3600*24; i++)
                {
                    _clock.tick();
                }
                _clock.PrintTime();

                //define actual result
                String actual = _clock.PrintTime();

                // Assert expected and actual are the same


                //Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected, actual);
            }
            [Test]
            public void AnyTimeAt29hris5()
            {
                //define expected result
                string expected = "05:00:00";

                //run the test
                for (int i = 0; i < 3600 * 29; i++)
                {
                    _clock.tick();
                }
                _clock.PrintTime();

                //define actual result
                String actual = _clock.PrintTime();

                // Assert expected and actual are the same


                //Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected, actual);
            }
            [Test]
            public void TestReset()
            {
                //define expected result
                string expected = "00:00:00";

                //run the test
                for (int i = 0; i < 3600; i++)
                {
                    _clock.tick();
                }
                _clock.reset();
                _clock.PrintTime();

                //define actual result
                String actual = _clock.PrintTime();

                // Assert expected and actual are the same


                //Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}