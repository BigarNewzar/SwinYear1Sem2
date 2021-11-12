using System;

namespace basic_C_sharp_difference
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Number _num = new Number(45);
            _num.AddFive();
            Console.WriteLine(_num.print());

        }
    }
}
