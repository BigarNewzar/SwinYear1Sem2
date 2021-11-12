using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public abstract class LibraryResource
    {
        private string _name;
        private bool _onLoan;

        public LibraryResource(string name)
        {
            _name = name;
            _onLoan = false; //not onloan when first created
        }

        public string Name
        {
            get
            {
                return _name; //both game and book used the same name property to get _name, so directly moved it here
            }
        }

        public virtual string Creator
        {
            get;    //the game and book both have creator method but one will get the _developer and other will get the _author. Thus I decided to make a virtual and override it there 
        }

        public bool OnLoan
        {
            get
            {
                return _onLoan;
            }

            set
            {
                _onLoan = value;
            }
        }
        //both game and book had same method and got and set _onLoad, so directly moved it here


    }
}
