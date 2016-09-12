using AOPTest.Domain.Entities;
using System;
using System.Collections.Generic;

namespace AOPTest.Domain.Services
{
    public interface IInvoiceCreatorService
    {
        Func<ItemWithQuantity, IEnumerable<ItemWithQuantity>, decimal> DiscountCaclulator { get; set; }

        Invoice CreateInvoice(IEnumerable<ItemWithQuantity> itemsWithQty);
    }
}