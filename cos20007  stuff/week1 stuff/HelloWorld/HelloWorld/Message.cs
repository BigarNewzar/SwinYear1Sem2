using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    public class Message
    {
        private string text;
        public Message(string txt)
        {
            text = txt;
        }

        public void Print() 
        {
            Console.WriteLine(text);
        }
    }
}
