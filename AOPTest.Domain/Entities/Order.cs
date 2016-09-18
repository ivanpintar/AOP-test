using System;

namespace AOPTest.Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public string ItemName { get; private set; }
        public decimal ItemPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal Tax { get; private set; }
        public decimal TotalPrice { get { return CalculateTotalPrice(); } }
        public decimal TotalTax { get { return CalculateTotalTax(); } }
        public decimal TotalDiscount { get { return CalculateTotalDiscount(); } }
        public DateTime Date { get; private set; }
        public decimal Discount { get; internal set; }

        protected Order() { }

        public Order(Item item, int quantity, decimal tax, decimal discount)
        {
            ItemName = item.Name;
            ItemPrice = item.Price;
            Quantity = quantity;
            Tax = tax;
            Discount = discount;
            Date = DateTime.Now;
        }

        private decimal CalculateTotalPrice()
        {
            return ItemPrice * Quantity - CalculateTotalDiscount() + CalculateTotalTax();
        }

        private decimal CalculateTotalTax()
        {
            return (ItemPrice * Quantity - CalculateTotalDiscount()) * Tax;
        }

        private decimal CalculateTotalDiscount()
        {
            return ItemPrice * Quantity * Discount;
        }

        public override string ToString()
        {
            return $"Order Id:{Id} ItemName:{ItemName} ItemPrice:{ItemPrice} TotalPrice:{TotalPrice}";
        }
    }
}