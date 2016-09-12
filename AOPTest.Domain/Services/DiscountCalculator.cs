using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Domain.Services
{
    public static class DiscountCalculator
    {
        public static decimal CalculateDiscount(ItemWithQuantity currentItem, IEnumerable<ItemWithQuantity> allItems)
        {
            // milk - if buying 4, pay only for 3
            if (currentItem.Item.Name == "Milk" && currentItem.Quantity >= 4)
            {
                return (decimal)0.25;
            }

            // pan - 10% on monday
            var panDay = DayOfWeek.Monday;
            if (currentItem.Item.Name == "Pan" && DateTime.Now.DayOfWeek == panDay)
            {
                return (decimal)0.10;
            }

            // if buying more than 10 items with no discount, 15% off on all items with no other discounts
            var otherItems = allItems
                .Where(x =>
                    !(x.Item.Name == "Milk" && x.Quantity >= 4) &&
                    !(x.Item.Name == "Pan" && DateTime.Now.DayOfWeek == panDay)).ToList();

            if (otherItems.Sum(x => x.Quantity) > 10)
            {
                return (decimal)0.15;
            }

            return 0;
        }
    }
}
