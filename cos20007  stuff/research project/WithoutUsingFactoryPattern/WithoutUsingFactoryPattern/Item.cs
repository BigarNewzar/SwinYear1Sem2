using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutUsingFactoryPattern
{
    public class Item           //Parent class for Gun and  sheild
    {
        private string _id;             //to store id of the item
        private string _name;           //to store name of the item
        private string _description;    //to store description of the item
        //parent class constructor takes in the id, name, desc to make the item
        public Item(string id, string name,
        string desc) 
        {
            _id = id;
            _name = name;
            _description = desc;
            
        }

        //getter property for id so that it can be called in child class
        public string id
        {    get { return _id; }        }

        //getter property for name so that it can be called in child class
        public string name
        {     get { return _name; }       }

        //getter property for description so that it can be called in child class
        public string desc
        {     get { return _description; }       }

        //virtual method that will be overriden by child class
        public virtual string itemDetails()
        {   string fd = "blah blah";
            return fd;
        }
    }
}
