using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public interface IHaveInventory
    {
        public GameObject Locate(string id);
        
        public string Name 
        {
                get;
        }

        //added it to get access to inventory in transfer command
        Inventory Inventory
        {
            get;
        }
    }
}
