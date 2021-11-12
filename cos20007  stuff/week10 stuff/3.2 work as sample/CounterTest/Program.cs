using System;
using System.Threading; //namespace to be able to use the threading for delay
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterTest
{
    class Program
    {


        static void Main(string[] args)
        {
            Clock myClock = new Clock();

            for (int i=0; i<24*60*60 ;i++)
            {
                myClock.tick();

                
                Thread.Sleep(1000);//waits 1000 miliseconds before running the loop again. 
                                //need to use namespace system thread or else it wont recognise it!

                Console.WriteLine(myClock.PrintTime());

                
            }
        } 
    }
}
