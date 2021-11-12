using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingFactoryPattern
{
    public class Sheild:IItem
    {
        private string _id;             //to store id of the sheild
        private string _name;           //to store name of the sheild
        private string _description;    //to store description of the sheild

        //Sheild takes in id, name, desc to make itself
        public Sheild( string id, string name, string desc)
        {
            _id = id;
            _name = name;
            _description = desc;
        }

        //implimenting the method in the interface
        public string itemDetails()
        {
            string fd = "This is a Sheild\nit's id is " + this._id + " and its name is " + this._name + "\nDetails about it: " + this._description;
            return fd;
        }

    }
}
