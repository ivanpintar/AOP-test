using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Domain.Entities
{
    public class Item
    {
        public int Id { get; private set; }
        public enum ItemType { Food, Other }
        public ItemType Type { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        private Item()
        {

        }

        public Item(string name, decimal price, ItemType type)
        {
            Type = type;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{GetType().Name} Id:{Id} Item:{Name} Price:{Price} Type:{Type}";
        }
    }
}
