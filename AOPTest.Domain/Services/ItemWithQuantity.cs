using AOPTest.Domain.Entities;

namespace AOPTest.Domain.Services
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

        public override string ToString()
        {
            return $"{GetType().Name} ItemName:{Item.Name} QTY:{Quantity}";
        }
    }
}
