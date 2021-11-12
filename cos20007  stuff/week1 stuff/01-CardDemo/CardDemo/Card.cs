using System;
namespace CardDemo
{
	public class Card
	{
		private string _Rank;
		private string _Suit;
		private bool _FaceUP;

		public bool getFaceUp()
		{
			return _FaceUP;
		}

		public bool FaceUp
		{
			get
			{
				return _FaceUP;
			}
		}

		public Card(string rank, string suit)
		{
			_Rank = rank;
			_Suit = suit;
			_FaceUP = false;
		}

		public void Flip()
		{
			_FaceUP = !_FaceUP;
		}

		public void printDetails()
		{
			if (_FaceUP)
				Console.WriteLine("{0} {1}", _Rank, _Suit);
			else
				Console.WriteLine("*****");
		}

	}
}

