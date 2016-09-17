using AOPTest.AOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; private set; }
        public IEnumerable<Order> Orders { get; private set; }

        public DateTime Date { get; private set; }

        public decimal TotalPrice { get; private set; }

        private Invoice()
        {

        }

        [LoggingInterceptor]
        public Invoice(IEnumerable<Order> orders)
        {
            Orders = orders;
            Date = DateTime.Now;
            CalculateTotal();
        }

        [LoggingInterceptor]
        private void CalculateTotal()
        {
            TotalPrice = Orders
                .Select(x => x.TotalPrice)
                .Sum();
        }

        public override string ToString()
        {
            return $"{GetType().Name} Id:{Id} TotalPrice:{TotalPrice}";
        }
    }
}
