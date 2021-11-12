using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class LookCommand : Command
    {
        public LookCommand() : base (new string[] { "look"} )
        {

        }
        public override string Execute(Player p, string[] text)
        {
            //1st element not "look"
            if (text[0] != "look")
            {
                return "Error in look input";
            }

            //not 3 or 5 elements
            if (text.Length != 3 && text.Length != 5)
            {
                return "I don't know how to look like that";
            }

            

            //2nd element not "at"
            if (text[1] != "at")
            {
                return "What do you want to look at?";
            }

            //5 element in array, but 4th word not "in"
            if (text.Length == 5 && text[3] != "in")
            {
                return "What do you want to look in?";
            }

            //3 element in array, so container is player
            //or if 5 elements in array and last word is inventory
            if (text.Length == 3 || text.Length == 5 && text[4] == "inventory")
            {
               return LookAtIn(text[2] ,p);
            }

            //5 element in array, so container is the 5th element
            if(text.Length == 5)
            {
                FetchContainer(p, text[4]);
            }


            //to prevent it from returning smth when bag or container is not there (ie avoiding null reference error)
            //most store it in a variable with datatype IHaveInventory before comparing whether null or not!
            IHaveInventory ContainerNull = FetchContainer(p, text[4]);
            if (ContainerNull == null)
            {
                return "I can't find the " + text[4];
            }

            return LookAtIn(text[2], FetchContainer(p, text[4]));

        }

        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            
            //Note: "as" can be used to convert data types from one form to another!
            return p.Inventory.Fetch(containerId) as IHaveInventory;
            
        }


        private string LookAtIn (string thingId, IHaveInventory container)
        {
            //locate in IHaveInventory is set as GameObject type!
            GameObject item = container.Locate(thingId);
            if (item == null)
            {
                return "I can't find the " + thingId;
            }
            else
                return item.FullDescription;
        }


    }
}
