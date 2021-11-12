using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basic_C_sharp_difference
{
    public class Number
    {
        private int _number;
        public Number(int num) 
        {
            _number = num;
        }

        public void AddFive()
        {
            _number = _number + 5;
        }
        public int print()
        {
            return _number;
        }
    }
}
