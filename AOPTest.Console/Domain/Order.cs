using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Console.Domain
{
    public class Order
    {
        public Item Item { get; private set; }
        public int Quantity { get; private set; }
        public decimal Tax { get; private set; }
        public decimal TotalPrice { get; private set; }
        public decimal TotalTax { get; private set; }
        public decimal TotalDiscount { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Discount { get; internal set; }

        public Order(Item item, int quantity, decimal tax, decimal discount)
        {
            Item = item;
            Quantity = quantity;
            Tax = tax;
            Discount = discount;
            Date = DateTime.Now;

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalDiscount = Item.Price * Quantity * Discount;
            TotalTax = (Item.Price * Quantity - TotalDiscount) * Tax;
            TotalPrice = Item.Price * Quantity  - TotalDiscount + TotalTax;
        }
    }
}
