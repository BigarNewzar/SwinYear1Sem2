using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingFactoryPattern
{
    //a common interface for items where all items need to have method itemDetails, with no parameter, that will return a string type variable
    public interface IItem
    {
        string itemDetails();
    }
}
