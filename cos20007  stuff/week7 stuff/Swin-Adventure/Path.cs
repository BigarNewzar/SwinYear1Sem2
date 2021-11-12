using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Path:GameObject
    {
        private Location _location;
        
		
    

	public Path(string[] id, Location location, string name, string desc) : base(id,name,desc)
	{
		_location = location;
			
		
	}

	

	public override string FullDescription
		{
		get
		{
				string Fd = "You head " + this.FirstID + "\n" + base.FullDescription + "\nYou have arrived in a " + Location.Name;

				return Fd;
		}

	}
		

		

		public Location Location
		{
			get
			{
				return _location;
			}
		}

		public void Move(Player player)
		{
			player.ChangeLocation(_location);



		}

	}
}


