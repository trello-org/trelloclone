using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProjectTests.Domain
{
	public class CreditCard
	{
		public DateTime ExpirationDate { get; set; }
		public int Balance { get; set; }

		public Order PayForOrder(Order order)
		{
			Validate();
			order.Validate();
			return ProcessPayment(order);
		}

		private Order ProcessPayment(Order order)
		{
			int sum = 0;
			foreach (Item item in order.items)
				sum += item.Price * item.Amount;

			if (Balance < sum) throw new InsufficientFundsException("Insufficient balance on credit card.");

			Balance -= sum;
			order.IsPaid = true;
			return order;
		}

		public void Validate()
		{
			if (ExpirationDate < DateTime.Now) throw new ExpiredCreditCardException("Credit card has expired.");
			if (Balance < 0) throw new InvalidCreditCardBalanceException("Invalid balance on credit card.");
			Console.WriteLine("Credit card has been successfully validated.");
		}

		
	}

	public class InsufficientFundsException : Exception
	{
		public InsufficientFundsException(string message) : base(message)
		{

		}
	}

	public class ExpiredCreditCardException : Exception
	{
		public ExpiredCreditCardException(string message) : base(message)
		{

		}
	}

	public class InvalidCreditCardBalanceException : Exception
	{
		public InvalidCreditCardBalanceException(string message) : base(message)
		{

		}
	}


}
