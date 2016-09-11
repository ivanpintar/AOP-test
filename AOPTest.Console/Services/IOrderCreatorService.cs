using System;
using System.Collections.Generic;
using AOPTest.Console.Domain;

namespace AOPTest.Console.Services
{
    public interface IOrderCreatorService
    {
        Dictionary<Item.ItemType, decimal> TaxList { get; set; }

        Order CreateOrder(ItemWithQuantity itemWithQty, decimal discount);
    }
}