using AOPTest.AOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public string ItemName { get; private set; }
        public decimal ItemPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal Tax { get; private set; }
        public decimal TotalPrice { get; private set; }
        public decimal TotalTax { get; private set; }
        public decimal TotalDiscount { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Discount { get; internal set; }

        private Order() { }

        public Order(Item item, int quantity, decimal tax, decimal discount)
        {
            ItemName = item.Name;
            ItemPrice = item.Price;
            Quantity = quantity;
            Tax = tax;
            Discount = discount;
            Date = DateTime.Now;

            TotalDiscount = CalculateTotalDiscount(item.Price, quantity, discount);
            TotalTax = CalculateTotalTax(item.Price, quantity, TotalDiscount, tax);
            TotalPrice = CalculateTotalPrice(item.Price, quantity, TotalDiscount, TotalTax);
        }

        [LoggingInterceptor]
        private decimal CalculateTotalPrice(decimal price, int quantity, decimal totalDiscount, decimal totalTax)
        {
            return price * quantity - totalDiscount + TotalTax;
        }

        [LoggingInterceptor]
        private decimal CalculateTotalTax(decimal price, int quantity, decimal totalDiscount, decimal tax)
        {
            return (price * quantity - totalDiscount) * tax;
        }

        [LoggingInterceptor]
        private decimal CalculateTotalDiscount(decimal price, int quantity, decimal discount)
        {
            return price * quantity * discount;
        }

        public override string ToString()
        {
            return $"{GetType().Name} Id:{Id} ItemName:{ItemName} ItemPrice:{ItemPrice} TotalPrice:{TotalPrice}";
        }
    }
}
