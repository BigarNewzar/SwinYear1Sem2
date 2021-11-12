using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public class Game : LibraryResource
    {
        private string _developer;
        private string _contentRating;

        public Game(string name, string creator, string rating) : base(name)
        {
            _developer = creator;
            _contentRating = rating;
        }

        public override string Creator
        {
            get
            {
                return _developer;
            }
        }

        public string ContentRating
        {
            get
            {
                return _contentRating;
            }
        }
    }
}

