using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class IdentifiableObject
    {
        private List<string> _identifiers;

        public IdentifiableObject(string[] idents)
        {
            _identifiers = new List<string>() ;

            /*Testing stuff
             * foreach (string i in idents)
            {
                Console.WriteLine("before");
                Console.WriteLine(i);

            }*/

            idents = Array.ConvertAll(idents, d => d.ToLower());

            /*Testing stuff
             * foreach (string i in idents)
            {
                Console.WriteLine("after");
                Console.WriteLine(i);
            
            }*/
            
            //note to self, Array.ConvertAll will let you modify all the elements in the array, but the modification will be applied onto all the elements in the array, so beware where you use it! 
           

            _identifiers.AddRange(idents);
        }

        public Boolean AreYou(string id)
        {
            if (_identifiers.Contains(id.ToLower()))
            {
                return true;
            }
            else
                return false;
        }
        public string FirstID
        {
            get
            {
                return _identifiers.FirstOrDefault();

            }
        }

        public void AddIdenfier(string id)
        {
            
            _identifiers.Add(id.ToLower());
        }
    }
}
