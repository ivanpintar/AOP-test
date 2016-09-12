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

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalDiscount = ItemPrice * Quantity * Discount;
            TotalTax = (ItemPrice * Quantity - TotalDiscount) * Tax;
            TotalPrice = ItemPrice * Quantity  - TotalDiscount + TotalTax;
        }

        public override string ToString()
        {
            return $"{GetType().Name} Id:{Id} ItemName:{ItemName} ItemPrice:{ItemPrice} TotalPrice:{TotalPrice}";
        }
    }
}
