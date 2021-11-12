using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class MoveCommand:Command
    {
        public MoveCommand() : base(new string[] { "move", "go", "head", "leave" })
        {
        }
        public override string Execute(Player player, string[] text)
        {
            if (text.Length == 2 && ((text[0] == "move") || (text[0] == "go") || (text[0] == "head")) || (text[0] == "leave"))
            {
                Location currentLocation = player.Location;
                Path nextPath = currentLocation.GetPathIndividual(text[1]);
                if (nextPath == null)
                {
                    return "path id is invalid";
                }
                else
                {
                    nextPath.Move(player);
                    return nextPath.FullDescription;
                }
            }
            else 
            {
                return "I don't know how to move like that";
            }
		}
    }
}
