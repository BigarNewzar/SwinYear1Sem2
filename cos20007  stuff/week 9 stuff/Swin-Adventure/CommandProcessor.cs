using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swin_Adventure
{
    public class CommandProcessor
    {
		private List<Command> _command;

		public CommandProcessor() 
		{   //add all the commands to command list first
			_command = new List<Command>();
			_command.Add(new LookCommand());
			_command.Add(new MoveCommand());
			_command.Add(new TransferCommand());
			_command.Add(new QuitCommand());

		}


		public string Execute(Player player, string[] text)
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
