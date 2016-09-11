using AOPTest.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Console.Services
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

        public Order CreateOrder(ItemWithQuantity itemWithQty, decimal discount)
        {
            var item = itemWithQty.Item;
            var quantity = itemWithQty.Quantity;
            var tax = TaxList[item.Type];

            throw new Exception("Bla");

            return new Order(item, quantity, tax, discount);
        }

    }
}
