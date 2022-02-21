using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaymentProjectTests.Domain
{
	public class Item
	{
		public int Price { get; set; }
		public string Name { get; set; }
		public int Amount { get; set; }

		public void Validate()
		{
			Regex rx = new Regex(@"[a-zA-Z0-9]");
			if (!rx.IsMatch(Name)) throw new InvalidItemNameException("Validation error on item name.");

			if (Price <= 0) throw new InvalidItemPriceException("Invalid item price.");

			if (Amount <= 0) throw new InvalidItemAmountException("Item count must be > 0");

			Console.WriteLine("Item has been successfully validated.");
		}


	}
	public class InvalidItemAmountException : Exception
	{
		public InvalidItemAmountException(string message) : base(message)
		{

		}
	}

	public class InvalidItemNameException : Exception
	{
		public InvalidItemNameException(string message) : base(message)
		{

		}
	}

	public class InvalidItemPriceException : Exception
	{
		public InvalidItemPriceException(string message) : base(message)
		{

		}
	}
}
