using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Location: GameObject, IHaveInventory

    {
        private Inventory _inventory;
		private string _description;

        public Location(string[] ids, string name, string desc ):base(ids, name, desc)
        {
			_inventory = new Inventory();
			_description = desc;
		}

		public GameObject Locate(string id)
		{
			if (AreYou(id))
			{
				return this;
			}
			else
			{
				return _inventory.Fetch(id);
			}
		}

		public Inventory Inventory
		{
			get { return _inventory; }
		}

		public override String FullDescription
		{

			get
			{


				string Fd = "You are in the " + this.Name + "\n" + this._description + "\nIn this room you can see\n" + _inventory.ItemList;

				return Fd;

			}

		}

		
	}
}
