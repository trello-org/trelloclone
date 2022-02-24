using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaymentProjectTests.Domain
{
	public class Math
	{
		public int A { get; private set; }
		public int B { get; private set; }

		public int Divide(int a, Positive b)
		{
			return a / b;
		}
	}

	public class Positive
	{
		public int Value { get; private set; }
		public List<string> Errors { get; private set; } = new List<string>();

		public Positive(int value)
		{
			if (value <= 0)
			{
				Errors.Add("Cannot divide by zero or less.");
			}

			if (Errors.Any())
				return;

			Value = value;
		}

		public static implicit operator int(Positive p) => p.Value;
	}

	public class Order
	{
		public List<Item> items { get; set; }
		public bool IsPaid { get; set; }

		public void AddItem(Item item)
		{
			item.Validate();
			CheckItemUniqueness(item);
			items.Add(item);
		}
		
		private void CheckItemUniqueness(Item newItem)
		{
			var item = items.Single(i => i.Name == newItem.Name);
			if (item != null) throw new ItemAlreadyInOrderException("The item has already been added to the order.");
		}

		public void Validate()
		{
			if (IsPaid) throw new OrderPaymentException("Order is already paid");
			if (items.Count < 5) throw new InvalidItemCountException("There must be at least 5 items in an order.");

			Console.WriteLine("Order has been successfully validated.");
		}

	}

	public class ItemAlreadyInOrderException : Exception
	{
		public ItemAlreadyInOrderException(string message) : base(message)
		{

		}
	}

	public class InvalidItemCountException : Exception
	{
		public InvalidItemCountException(string message) : base(message)
		{

		}
	}

	public class OrderPaymentException : Exception
	{
		public OrderPaymentException(string message) : base(message)
		{

		}
	}


}
