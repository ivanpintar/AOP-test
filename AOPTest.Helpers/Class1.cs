using AOPTest.Domain.Entities;
using AOPTest.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOPTest.Helpers
{
    public static class HelperMethods
    {
        public static void DisplayInvoice(Invoice invoice)
        {
            Console.WriteLine($"Date: {invoice.Date}");
            Console.Write($"{"ITEM".PadRight(20)}");
            Console.Write($"{"QTY".PadRight(7)}");
            Console.Write($"{"PRICE".PadRight(7)}");
            Console.Write($"{"DISCOUNT".ToString().PadRight(14)}");
            Console.Write($"{"TAX".PadRight(14)}");
            Console.Write($"{"TOTAL".PadRight(7)}\n");
            Console.WriteLine($"==========================================================================");
            foreach (var o in invoice.Orders)
            {
                Console.Write($"{o.ItemName.PadRight(20)}");
                Console.Write($"{o.Quantity.ToString().PadRight(7)}");
                Console.Write($"{o.ItemPrice.ToString().PadRight(7)}");
                Console.Write($"%{(o.Discount * 100).ToString().PadRight(6)}");
                Console.Write($"{o.TotalDiscount.ToString().PadRight(7)}");
                Console.Write($"%{(o.Tax * 100).ToString().PadRight(6)}");
                Console.Write($"{o.TotalTax.ToString().PadRight(7)}");
                Console.Write($"{o.TotalPrice.ToString().PadRight(7)}\n");

            }
            Console.WriteLine($"TOTAL: {invoice.TotalPrice}");
        }
    }
}
