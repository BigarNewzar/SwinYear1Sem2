﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private Location _location;
        


        public Player(string name, string desc) : base(new string[] { "me", "inventory" }, name, desc)
        {
            _inventory = new Inventory();
            


        }

        public Location Location
        {
            get { return _location; }

            set
            {
                _location = value;
            }
        }

        public void ChangeLocation(Location location)
        {
            _location = location;
        }

        public GameObject Locate(string id)
        {
            if (base.AreYou(id))
            {
                return this; //To make sure it returns "me" or player! 

            }

            else if (_inventory.HasItem(id))
            {
                return _inventory.Fetch(id);
            }

            else
            {
                if (_location != null)
                {
                    return _location.Locate(id);
                }
                else
                {
                    return null;
                }
            }
        }

        public override string FullDescription
        {
            get
            {

                string Fd = "You are " + Name + " "+ base.FullDescription + "\n You are carrying:\n" + _inventory.ItemList;

                return Fd;

            }
        }

        public Inventory Inventory
        {
            get { return _inventory; }
        }

        

    }
}
