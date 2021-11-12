using System;

namespace HelloWorld
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Message myMessage;
            myMessage = new Message("Hello World - from Message Object");
            myMessage.Print();

            //Console.ReadLine();

            Message[] messages = new Message[4];
            messages[0] = new Message("Good name. Ever heard of Chris Pratt? ");
            messages[1] = new Message("Way too common name. Care to get a new one?");
            messages[2] = new Message("Bro, dont break the 4th wall just for a name test.");
            messages[3] = new Message("That is a silly name");

            Console.WriteLine("Enter Name: ");
            string name = Console.ReadLine();
            
            if (name.ToLower() == "chris")
            {
                messages[0].Print();

            }

            else if (name.ToLower() == "andrew")
            {
                messages[1].Print();

            }

            else if (name.ToLower() == "deadpool")
            {
                messages[2].Print();

            }

            else
            {
                messages[3].Print();

            }








        }
    }
}
