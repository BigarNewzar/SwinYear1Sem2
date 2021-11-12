#pragma once
#include <string>
#include <iostream> 
using namespace std;

class Counter
{	
private:
	int _count;
	string _name;
public:              
    Counter(string name) {  
        _name = name;
        _count = 0;
    }
    void Increment()
    {
        _count++;

    }
    void Reset()
    {
        _count = 0;

    }

    
    
    string getName()
    {
            return _name;
    }
    string setName(int value)
    {
            _name = value;
    }
    

        int getTicks()
        {
                return _count;
        }

        
};

