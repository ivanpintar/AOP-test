using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Console.Domain
{
    public class Invoice
    {
        public IEnumerable<Order> Orders { get; private set; }

        public DateTime Date { get; private set; }

        public decimal TotalPrice { get; private set; }

        public Invoice(IEnumerable<Order> orders)
        {
            Orders = orders;
            Date = DateTime.Now;
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalPrice = Orders
                .Select(x => x.TotalPrice)
                .Sum();
        }
    }
}
