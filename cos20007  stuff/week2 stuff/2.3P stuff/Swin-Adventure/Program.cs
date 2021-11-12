using System;

namespace Swin_Adventure
{
    public class Program
    {
        static void Main(string[] args)
        {
            IdentifiableObject id =
    new IdentifiableObject(new string[] { "ID1", "id2" });

            id.AddIdenfier("test");

            if (id.AreYou("test")==true) { Console.WriteLine("ok"); }
        }
    }
}
