using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Inventory
    {
        private List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
        }

        public bool HasItem(string id)
        {
            foreach (Item i in _items)
            {
                if (i.AreYou(id))
                {
                    return true;
                }

            }    
            return false;
                
            
        }

        public void Put(Item itm)
        {
            _items.Add(itm);
        }

        public Item Take(string id)
        {
            Item Stored = null;     //null can be used as a value!

            foreach (Item i in _items)
            {
                if (i.AreYou(id))
                {
                    Stored = i;
                }
            }
             _items.Remove(Stored);
                
             return Stored;
            

        }
        public Item Fetch(string id)
        {
            foreach (Item i in _items)
            {
                if (i.AreYou(id))
                {
                    return i;
                }
            }
            return null;

        }


        public string ItemList
        {
            get 
            { 
                
                string ListOfItem = "";
                
                foreach(Item i in _items)
                {   
                    // to get tab in c# use \t
                    //to get new line in c# use \n
                    ListOfItem += "\t" + i.ShortDescription + "\n";
                }

                if(ListOfItem == null)
                {
                    return "Item is not in the list of items";
                }

                return ListOfItem;
            }
  
        }

    }
}
