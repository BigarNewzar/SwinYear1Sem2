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
		private List<Path> _pathlist;

		public Location(string[] ids, string name, string desc ):base(ids, name, desc)
        {
			_inventory = new Inventory();
			_description = desc;
			_pathlist = new List<Path>();

		}
		public List<Path> PathList
		{
			get { return _pathlist; }

			set
			{
				_pathlist = value;
			}
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

		public Path GetPathIndividual(string id)
		{
			foreach (Path p in _pathlist)
			{
				if (p.AreYou(id))
					return p;
			}
			return null;
		}

		public void PutPath(Path path)
		{
			
				_pathlist.Add(path);
			
			
		}
		public override String FullDescription
		{

			get
			{

				string exits = "";


				exits += "\nThere are exits to the ";
				if (_pathlist.Count > 1)
				{
					exits += " and " + _pathlist;
				}
				else if (_pathlist.Count == 1)
				{
					exits += _pathlist[0].FirstID;

				}
				else 
				{
					exits = "There is no exit here!";
				}
				

				string Fd = "You are in the " + this.Name +  "\n" + exits + "\n" +  this._description + "\nIn this room you can see\n" + _inventory.ItemList;

				

				return Fd;

			}

		}

		
	}
}
