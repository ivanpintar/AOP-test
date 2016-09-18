using System;
using System.Collections.Generic;
using System.Linq;

namespace AOPTest.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; private set; }
        public virtual ICollection<Order> Orders { get; private set; }

        public DateTime Date { get; private set; }

        public decimal TotalPrice { get { return CalculateTotal(Orders); } }

        protected Invoice()
        {

        }

        public Invoice(IEnumerable<Order> orders)
        {
            Orders = orders.ToList();
            Date = DateTime.Now;
        }

        private decimal CalculateTotal(IEnumerable<Order> orders)
        {
            return orders
                .Select(x => x.TotalPrice)
                .Sum();
        }

        public override string ToString()
        {
            return $"Invoice Id:{Id} TotalPrice:{TotalPrice}";
        }
    }
}