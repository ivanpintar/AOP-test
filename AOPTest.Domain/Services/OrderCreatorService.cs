using AOPTest.AOP;
using AOPTest.Domain.Entities;
using System;
using System.Collections.Generic;

namespace AOPTest.Domain.Services
{
    public class OrderCreatorService : IOrderCreatorService
    {
        public Dictionary<Item.ItemType, decimal> TaxList { get; set; }

        public OrderCreatorService()
        {
            TaxList = new Dictionary<Item.ItemType, decimal>
            {
                {  Item.ItemType.Food, (decimal)0.10 },
                {  Item.ItemType.Other, (decimal)0.20 }
            };
        }

        [LoggingInterceptor]
        public Order CreateOrder(ItemWithQuantity itemWithQty, decimal discount)
        {
            var item = itemWithQty.Item;
            var quantity = itemWithQty.Quantity;
            var tax = TaxList[item.Type];

            return new Order(item, quantity, tax, discount);
        }

    }
}
