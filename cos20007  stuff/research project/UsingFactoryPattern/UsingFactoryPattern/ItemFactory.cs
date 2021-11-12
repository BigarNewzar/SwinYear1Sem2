using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingFactoryPattern
{
    public class ItemFactory
    {
        private IItem _item; //utilising interface to instanciate the specific items
        public IItem createItem(string type)
        {
            _item = null;

            //if gun type then make a new gun object
            if (type == "Gun") {
                _item = new Gun("A45", "revolver", "this is a good revolver");
           }

            //if sheild type then make a new sheild object
            if (type == "Sheild")
            {
                _item = new Sheild("B23", "Electric Sheild", "this is an electric shield");
            }

            return _item; //return the created object
        }

    }
}
