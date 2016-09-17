using AOPTest.AOP;
using AOPTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOPTest.Domain.Services
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

        [LoggingInterceptor]
        public Invoice CreateInvoice(IEnumerable<ItemWithQuantity> itemsWithQty)
        {
            var orders = itemsWithQty
                .Select(x => _orderCreatorSvc.CreateOrder(x, DiscountCaclulator(x, itemsWithQty.ToList())))
                .ToList();

            return new Invoice(orders);
        }
    }
}
