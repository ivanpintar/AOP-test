using System.Collections.Generic;
using AOPTest.Domain.Entities;

namespace AOPTest.Domain.Services
{
    public interface IOrderCreatorService
    {
        Dictionary<Item.ItemType, decimal> TaxList { get; set; }

        Order CreateOrder(ItemWithQuantity itemWithQty, decimal discount);
    }
}