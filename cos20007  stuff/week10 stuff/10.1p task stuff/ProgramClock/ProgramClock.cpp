// ProgramClock.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include <string>
#include <iostream> 
#include "Counter.h"//to use the counter class objects here!
#include "Clock.h"
#include <iomanip>
#include <dos.h>//to use delay
#include <conio.h> //for getch()

#include <chrono>
#include <thread>

using namespace std;

void main(string[])
{   

    Clock myClock;
    for (int i = 0; i < 24 * 60 * 60;i++)
    {   

        myClock.tick();
        cout<< myClock.PrintTime();
        std::this_thread::sleep_for(std::chrono::seconds(1));// using chrono and thread to get the delay
        //reference:https://stackoverflow.com/questions/46230710/how-to-add-a-delay-to-code-in-c
        
                                                             
        //delay(1000); //for some reason this delay wasnt working, maybe it wasnt finding dos.h for some reason?
    }
    
}
