using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterTest
{
    public class Clock
    {
        private Counter _sec;
        private Counter _min;
        private Counter _hr;
        

        public Clock()
        {
            _sec = new Counter("sec");
            _min = new Counter("min");
            _hr = new Counter("hr");
           
        }

        public void tick()
        {
           
            _sec.Increment();
            if(_sec.Ticks >59)
            {
                _sec.Reset();
                _min.Increment();

            }

            if (_min.Ticks > 59)
            {
                _min.Reset();
                _hr.Increment();

            }

            if (_hr.Ticks > 23)
            {
                _hr.Reset();

            }
        }

        public string PrintTime()
        {
            

            return String.Format("{0:D2}", _hr.Ticks) + ":" + String.Format("{0:D2}", _min.Ticks) + ":" + String.Format("{0:D2}", _sec.Ticks);
        }

        public void reset()
        {
            _sec.Reset();
            _min.Reset();
            _hr.Reset();
        }


    }
}
