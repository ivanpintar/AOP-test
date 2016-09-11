using AOPTest.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Console.Services
{
    public class InvoiceCreatorService : IInvoiceCreatorService
    {
        private IOrderCreatorService _orderCreatorSvc;
        public Func<ItemWithQuantity, IEnumerable<ItemWithQuantity>, decimal> DiscountCaclulator { get; set; }

        public InvoiceCreatorService(IOrderCreatorService orderCreatorSvc)
        {
            _orderCreatorSvc = orderCreatorSvc;
            DiscountCaclulator = (x, y) => 0;
        }

        public Invoice CreateInvoice(IEnumerable<ItemWithQuantity> itemsWithQty)
        {
            var orders = itemsWithQty
                .ToList()
                .Select(x => _orderCreatorSvc.CreateOrder(x, DiscountCaclulator(x, itemsWithQty.ToList())));

            return new Invoice(orders);
        }
    }
}
