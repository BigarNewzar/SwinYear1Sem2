using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Swin_Adventure;

namespace SwinAdventureGUI
{
    public partial class Form1 : Form
    {
        private string _name, _desc;
        private Player _player;
        private Item _sword, _torch, _orb;
        private Bag _bag;
        private Location _smallcloset,_hallway;
        private Path _hallway_path;
       
        private CommandProcessor command;
        private string _inputText;
        public Form1()
        {
            InitializeComponent();

            textBox1.Text = "Welcome to Swin Adventure! \r\n You have arrived in the Hallway ";

            _name = "Player";
            _desc = "someone who is playing the game";


           _player = new Player(_name, _desc);

            _sword = new Item(new string[] { "sword" }, "sword", "an old rusty sword");

            _torch = new Item(new string[] { "torch" }, "torch", "a torch to light the way");

            _player.Inventory.Put(_sword);

            _player.Inventory.Put(_torch);


            _bag = new Bag(new string[] { "bag" }, "bagpack", "initial bagpack for player");

            _player.Inventory.Put(_bag);



            _orb = new Item(new string[] { "orb" }, "shiny orb", "a mysterious orb that seems to glow from within");

            _bag.Inventory.Put(_orb);




            _smallcloset = new Location(new String[] { "small closet", "smallcloset" }, "small closet",
                                                    "A small dark closet, with an odd smell");

            _hallway_path = new Path(new String[] { "South" }, _smallcloset,
                                        "hallway_path", "You go through a door.");
            _hallway = new Location(new String[] { "hallway", "long hallway" }, "hallway",
                                            "This is a long well lit hallway");

            _player.Location = _hallway;

            _hallway.PutPath(_hallway_path);
            _hallway.GetPathIndividual(_hallway_path.FirstID);//not sure if this is really needed

            command = new CommandProcessor();

          

        }

        string[] StringToSubstring(string text)
        {
            char delimiterChar = ' ';// space will be used to seperate them
            return text.Split(delimiterChar);// split will split the string into substrings
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _inputText = textBox2.Text;
            textBox1.Text = command.Execute(_player, StringToSubstring(_inputText)).Replace("\n", "\r\n");//basically, replace will find all instances of first string and replace with the 2nd string. Thus form will understand that \n means \r\n which is new line.


            
            if(_inputText == "quit")//once stored string is quit, after running the command (as it is outside else if part), it will kill the code 
            {
                System.Environment.Exit(0);
            }
            

            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
