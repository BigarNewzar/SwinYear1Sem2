#pragma once
#include <string>
#include <iostream> 
#include "Counter.h"//to use the counter class objects here!
#include <iomanip>

#include <stdio.h>
#include <sstream>
#include <math.h>
using namespace std;

class Clock

{
private:
    Counter _sec = Counter("sec");
    Counter _min = Counter("min");
    Counter _hr = Counter("hr");
public:
    Clock() 
    {
       _sec = Counter("sec");
       _min = Counter("min");
       _hr = Counter("hr");
    }
    void tick()
    {

        _sec.Increment();
        if (_sec.getTicks() > 59)
        {
            _sec.Reset();
            _min.Increment();

        }

        if (_min.getTicks() > 59)
        {
            _min.Reset();
            _hr.Increment();

        }

        if (_hr.getTicks() > 23)
        {
            _hr.Reset();

        }
    }

    string PrintTime()
    {//must have string stream included! also a few more libraries mentioned above! Otherwise program goes crazy and doesnt recognise ":" or 0.2f thingy
        //reference for setfill and string stream:
    //https://stackoverflow.com/questions/30330148/how-to-display-multiple-leading-zeros-for-floating-point-values-in-c
   // https://stackoverflow.com/questions/58695875/how-to-convert-seconds-to-hhmmss-millisecond-format-c
        stringstream ss;
        ss << std::setfill('0') << std::setw(2)<<("%0.2f", _hr.getTicks()) << ":" << std::setfill('0') <<  std::setw(2)<<("%0.2f", _min.getTicks()) << ":" << std::setfill('0') << std::setw(2) << ("%0.2f", _sec.getTicks())<<"\n";
        
        return ss.str();
        
    }

    void reset()
    {
        _sec.Reset();
        _min.Reset();
        _hr.Reset();
    }


};

