using AOPTest.Console.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Console.Services
{
    public class ItemWithQuantity
    { 
        public Item Item { get; private set; }
        public int Quantity { get; private set; }

        public ItemWithQuantity(Item item, int qty)
        {
            Item = item;
            Quantity = qty;
        }
    }
}
