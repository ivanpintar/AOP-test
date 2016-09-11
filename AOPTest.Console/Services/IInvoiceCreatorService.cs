using System;
using System.Collections.Generic;
using AOPTest.Console.Domain;

namespace AOPTest.Console.Services
{
    public interface IInvoiceCreatorService
    {
        Func<ItemWithQuantity, IEnumerable<ItemWithQuantity>, decimal> DiscountCaclulator { get; set; }

        Invoice CreateInvoice(IEnumerable<ItemWithQuantity> itemsWithQty);
    }
}