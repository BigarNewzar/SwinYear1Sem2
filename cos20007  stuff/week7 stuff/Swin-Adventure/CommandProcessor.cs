using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class CommandProcessor:Command
    {
		private List<Command> _command = new List<Command>();

		public CommandProcessor(string[] id, Command[] command) : base(id)
		{	//add all the commands to command list first
			foreach (Command c in command)
			{
				_command.Add(c);
			}
		}


		public override string Execute(Player player, string[] text)
		{	//now look though the commands in the list and execute them appropriately
			foreach (Command c in _command)
			{
				if (c.AreYou(text[0]))
				{
					return c.Execute(player, text);
				}
			}
			return "Command is Invalid.\nPlease look at the Command List before retyping";
		}
	}
}
