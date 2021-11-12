using System;

namespace CardDemo
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Card c = new Card("ACE", "HEARTS");

			c.printDetails();
			c.Flip();

			c.printDetails();

		}
	}
}
