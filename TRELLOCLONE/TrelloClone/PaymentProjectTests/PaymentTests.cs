using PaymentProjectTests.Domain;
using System;
using Xunit;
using Shouldly;
using System.Collections.Generic;

namespace PaymentProjectTests
{
	public class PaymentTests
	{
		[Fact]
		public void Item_with_an_empty_name_is_invalid()
		{
			var item = new Item() { Name = "", Price = 10, Amount = 1};
			Should.Throw<InvalidItemNameException>(() => item.Validate());
		}

		[Theory]
		[InlineData("!")]
		[InlineData("@")]
		[InlineData("#")]
		[InlineData("$")]
		[InlineData("%")]
		[InlineData("^")]
		[InlineData("&")]
		[InlineData("*")]
		[InlineData("(")]
		[InlineData(")")]
		[InlineData("_")]
		[InlineData("-")]
		[InlineData("=")]
		public void Item_with_non_alphanumeric_name_is_invalid(string name)
		{
			var item = new Item() { Name = name, Price = 10, Amount = 1 };
			Should.Throw<InvalidItemNameException>(() => item.Validate());
		}

		[Theory]
		[InlineData(-10)]
		[InlineData(0)]
		public void Item_with_non_positive_price_is_invalid(int price)
		{
			var item = new Item() { Name = "randomname", Price = price, Amount = 1 };
			Should.Throw<InvalidItemPriceException>(() => item.Validate());
		}

		[Theory]
		[InlineData(-5)]
		[InlineData(0)]
		public void Cannot_buy_zero_or_negative_amount_of_an_item(int amount) 
		{
			var item = new Item() { Name = "Item1", Price = 500, Amount = amount };
			Should.Throw<InvalidItemAmountException>(() => item.Validate());
		}

		[Theory]
		[InlineData(2008, 11, 22)]
		public void Credit_card_with_a_past_name_is_expired(int year, int month, int day)
		{
			var date = new DateTime(year, month, day);
			var creditCard = new CreditCard() { ExpirationDate = date, Balance = 1000000 };

			Should.Throw<ExpiredCreditCardException>(() => creditCard.Validate());
		}

		[Fact]
		public void Credit_card_with_negative_balance_is_invalid()
		{
			var creditCard = new CreditCard() { ExpirationDate = new DateTime(2025, 10, 10), Balance = -10000 };

			Should.Throw<InvalidCreditCardBalanceException>(() => creditCard.Validate());
		}

		[Fact]
		public void Creating_an_already_paid_order_is_forbidden()
		{
			var itemList = new List<Item>()
			{
				new Item() {Name = "item1", Price = 500, Amount = 1 },
				new Item() {Name = "item2", Price = 500, Amount = 1 },
				new Item() {Name = "item3", Price = 500, Amount = 1 },
				new Item() {Name = "item4", Price = 500, Amount = 1 },
				new Item() {Name = "item5", Price = 500, Amount = 1 }
			};

			var order = new Order() { IsPaid = true, items = itemList };
			Should.Throw<OrderPaymentException>(() => order.Validate());
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(4)]
		public void Order_with_less_than_five_items_is_invalid(int itemCount)
		{
			List<Item> itemList = new List<Item>();

			for(int i = 0; i<itemCount; i++)
			{
				itemList.Add(new Item() { Name = $"Newitem{i}", Price = 500, Amount = 1 });
			}

			var order = new Order() { IsPaid = false, items = itemList };

			Should.Throw<InvalidItemCountException>(() => order.Validate());

		}

		[Theory]
		[InlineData("item1", "item1")]
		public void Adding_duplicate_item_to_order_is_forbidden(string name1, string name2)
		{
			var itemList = new List<Item>()
			{
				new Item() { Name = name1, Price = 500, Amount = 1}
			};
			var order = new Order() { IsPaid = false, items = itemList };

			Should.Throw<ItemAlreadyInOrderException>(() => order.AddItem(new Item() { Amount = 500, Name = name2, Price = 500 }));
		}

		[Theory]
		[InlineData("")]
		[InlineData("!")]
		[InlineData("@")]
		[InlineData("#")]
		[InlineData("$")]
		[InlineData("%")]
		[InlineData("^")]
		[InlineData("&")]
		[InlineData("*")]
		[InlineData("(")]
		[InlineData(")")]
		[InlineData("_")]
		[InlineData("-")]
		[InlineData("=")]
		public void Adding_an_item_with_an_empty_or_non_alphanumeric_name_is_forbidden(string name)
		{
			var item = new Item() { Name = name, Price = 10, Amount = 1};
			var order = new Order() { items = new List<Item>() };
			Should.Throw<InvalidItemNameException>(() => order.AddItem(item));
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public void Adding_an_item_with_a_non_positive_price_is_forbidden(int price)
		{
			var item = new Item() { Amount = 1, Name = "itemname1", Price = price };
			var order = new Order() { IsPaid = false, items = new List<Item>() };

			Should.Throw<InvalidItemPriceException>(() => order.AddItem(item));

		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public void Adding_zero_or_less_items_to_an_order_is_forbidden(int amount)
		{
			var item = new Item() { Amount = amount, Name = "itemname1", Price = 500 };
			var order = new Order() { IsPaid = false, items = new List<Item>() };

			Should.Throw<InvalidItemAmountException>(() => order.AddItem(item));
		}

		[Theory]
		[InlineData(2008, 11, 22)]
		public void Paying_with_an_expired_credit_card_is_forbiden(int year, int month, int day)
		{
			var date = new DateTime(year, month, day);
			var creditCard = new CreditCard() { ExpirationDate = date, Balance = 1000000 };
			var order = new Order();

			Should.Throw<ExpiredCreditCardException>(() => creditCard.PayForOrder(order));
		}

		[Fact]
		public void Paying_with_a_credit_card_with_negative_balance_is_forbidden()
		{
			var creditCard = new CreditCard() { ExpirationDate = new DateTime(2025, 10, 10), Balance = -10000 };
			var order = new Order();

			Should.Throw<InvalidCreditCardBalanceException>(() => creditCard.PayForOrder(order));
		}

		[Fact]
		public void Paying_for_an_already_paid_order_is_forbidden()
		{
			var itemList = new List<Item>()
			{
				new Item() {Name = "item1", Price = 500, Amount = 1 },
				new Item() {Name = "item2", Price = 500, Amount = 1 },
				new Item() {Name = "item3", Price = 500, Amount = 1 },
				new Item() {Name = "item4", Price = 500, Amount = 1 },
				new Item() {Name = "item5", Price = 500, Amount = 1 }
			};

			var creditCard = new CreditCard() { ExpirationDate = new DateTime(2025, 10, 10), Balance = 10000 };

			var order = new Order() { IsPaid = true, items = itemList };
			Should.Throw<OrderPaymentException>(() => creditCard.PayForOrder(order));
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(4)]
		public void Paying_for_an_order_with_less_than_five_items_is_forbidden(int itemCount)
		{
			List<Item> itemList = new List<Item>();

			for (int i = 0; i < itemCount; i++)
			{
				itemList.Add(new Item() { Name = $"Newitem{i}", Price = 500, Amount = 1 });
			}

			var order = new Order() { IsPaid = false, items = itemList };
			var creditCard = new CreditCard() { ExpirationDate = new DateTime(2025, 10, 10), Balance = 10000 };

			Should.Throw<InvalidItemCountException>(() => creditCard.PayForOrder(order));

		}

		[Fact]
		public void Credit_card_with_insufficient_funds_is_declined()
		{
			List<Item> itemList = new List<Item>();

			for(int i = 0; i< 6; i++)
			{
				itemList.Add(new Item() { Name = $"item{i}", Price = 5000, Amount = 1 });
			}

			var creditCard = new CreditCard() { ExpirationDate = new DateTime(2025, 10, 10), Balance = 10000 };
			var order = new Order() { IsPaid = false, items = itemList };

			Should.Throw<InsufficientFundsException>(() => creditCard.PayForOrder(order));
		}



	}
}
