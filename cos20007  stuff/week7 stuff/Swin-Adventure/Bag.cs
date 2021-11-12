using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Bag : Item, IHaveInventory
    {
        private Inventory _inventory;

        public Bag(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _inventory = new Inventory();
        }

        public GameObject Locate(string id)
        {
            if (base.AreYou(id))
            {
                return this; //To make sure it returns bag! 

            }

            else if (_inventory.HasItem(id))
            {
                return _inventory.Fetch(id);
            }

            else
            {
                return null;
            }

        }

        public override String FullDescription
        {

            get
            {


                string Fd = "In the " + this.Name + " you can see: " + _inventory.ItemList;

                return Fd;

            }

        }

        public Inventory Inventory

        {

            get { return _inventory; }

        }


    }
}
