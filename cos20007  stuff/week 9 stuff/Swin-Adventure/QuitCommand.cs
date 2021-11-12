using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Swin_Adventure
{
    public class QuitCommand : Command
    {
        public QuitCommand() : base (new string[] { "quit"} )
        {

        }
        public override string Execute(Player p, string[] text)
        {

            if (text.Length == 1 && text[0] == "quit")//didnt set location in main code yet!
            {
                return "Bye.";
                
            }
            else 
            {
                return "I dont know how to quit like that";
            }

        }

       

    }
}
