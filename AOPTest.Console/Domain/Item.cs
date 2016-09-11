using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Console.Domain
{
    public class Item
    {
        public enum ItemType { Food, Other }
        public ItemType Type { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Item(string name, decimal price, ItemType type)
        {
            Type = type;
            Name = name;
            Price = price;
        }
    }
}
