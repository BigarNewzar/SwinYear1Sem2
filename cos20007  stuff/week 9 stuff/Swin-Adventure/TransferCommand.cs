using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class TransferCommand : Command
    {
        private IHaveInventory _container;
        public TransferCommand() : base(new string[] { "take", "pickup", "drop", "put" })
        {}
        public override string Execute(Player player, string[] text)
        {
            if (text.Length == 2 || text.Length == 4)
            {
                if (text.Length == 4)
                {
                    _container = FetchContainer(player, text[3].ToLower());
                }
                else
                {
                    _container = player.Location;
                }
                return trasnferObjToContainer(_container, player, text);
            }
            else
            {
                return "I don't know how to transfer like that";
            }
        }

        public string trasnferObjToContainer(IHaveInventory _container, Player player, string[] text)
        {
            if (_container != null)
            {
                if (_container.Inventory.HasItem(text[1].ToLower()) || player.Inventory.HasItem(text[1].ToLower()))
                {
                    if ((text[0].ToLower() == "drop") || (text[0].ToLower() == "put"))
                    {
                        _container.Inventory.Put(player.Inventory.Take(text[1].ToLower()));//merged item from player to container inventory part to reduce line count
                        return "You have put the " + text[1] + " in the " + _container.Name;
                    }
                    else
                    {
                        player.Inventory.Put(_container.Inventory.Take(text[1].ToLower()));
                        return "You have taken the " + text[1] + " from the " + _container.Name;
                    }

                }
                else
                {
                    return "I cannot find the " + text[1];
                }

            }
            else
            {
                return "I cannot find the " + _container;
            }
        }
        
        public GameObject PickUp(IHaveInventory container, string id)
        {
            return container.Locate(id);
        }
        public IHaveInventory FetchContainer(Player player, string containerID)
        {
            //return player.Inventory.Fetch(containerID) as IHaveInventory;
            GameObject tempStore = player.Locate(containerID);
            if (tempStore == null && player.Location.AreYou(containerID))
            {
                tempStore = player.Location;
            }
            _container = tempStore as IHaveInventory;
            return _container;
        }
    }
}
