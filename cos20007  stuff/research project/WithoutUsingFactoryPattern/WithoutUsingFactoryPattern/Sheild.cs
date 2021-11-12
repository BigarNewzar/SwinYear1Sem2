using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutUsingFactoryPattern
{
    public class Sheild : Item
    {
        //Child class constructor inherits id, name, desc from parent class constructor
        public Sheild(string id, string name, string desc) : base(id, name, desc)
        {
        }

        //overriding the method provided by parent class to fulfill its own purpose
        public override string itemDetails()
        {
            string fd = "This is a Sheild\nit's id is " + base.id + " and its name is " + base.name + "\nDetails about it: " + base.desc;
            return fd;
        }

    }
}
