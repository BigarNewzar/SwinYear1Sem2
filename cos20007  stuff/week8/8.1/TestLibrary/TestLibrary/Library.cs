using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{

    public class Library
    {
        private string _name;   //added it to store the name
        private List<LibraryResource> _libraryList;

        public Library(string name)
        {
            _name = name;
            _libraryList = new List<LibraryResource>();
        }

        public void AddResource(LibraryResource resource)
        {
            _libraryList.Add(resource);
        }

        public bool HasResource(string name)
        {
            foreach (LibraryResource l in _libraryList)
            {
                if (l.Name == name && l.OnLoan == false)
                {
                    return true;//able to get true if (name matches and not on load)
                }
            }

            return false;
        }
    }
}
