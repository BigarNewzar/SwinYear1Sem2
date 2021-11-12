using System;


namespace CounterTest
{
    class Program
    {

        private static void PrintCounters(Counter[] counters)
        {
            foreach (Counter c in counters)
            {
                Console.WriteLine("{0} is {1}", c.Name, c.Ticks);
            }
        }
        static void Main(string[] args)
        {

            Counter[] myCounters = new Counter[3];
            myCounters[0] = new Counter( "Counter 1");
            myCounters[1] = new Counter("Counter 2");
            myCounters[2] = myCounters[0];
            //he showed diagram upto this, need to focus on next part's variables and objects

            int i;

            for (i = 0; i < 5; ++i)
            {
                myCounters[0].Increment();
            }

            for (i = 0; i < 10; ++i)
            {
                myCounters[1].Increment();
            }
            
            PrintCounters(myCounters);
            myCounters[2].Reset();//he also showed this part in diagram with crossed out part
            PrintCounters(myCounters);
        }
    }
}
