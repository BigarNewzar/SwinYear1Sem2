using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public abstract class Command:IdentifiableObject
    {
        public Command(string[] id):base(id)
        {

        }
        public abstract string Execute(Player p, string[] text);
        
       

    }
}
