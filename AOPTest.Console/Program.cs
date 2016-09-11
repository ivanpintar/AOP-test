using AOPTest.Console.Domain;
using AOPTest.Console.Services;
using System.Collections.Generic;
using System;
using System.Linq;
using Autofac;
using Autofac.Extras.DynamicProxy;
using AOPTest.Console.AOP;

namespace AOPTest.Console
{
    class Program
    {
        private static IContainer _container;

        static void Main(string[] args)
        {
            CreateContainer();

            var invoiceSvc = _container.Resolve<IInvoiceCreatorService>();
            invoiceSvc.DiscountCaclulator = DiscountCalculator;

            var itemsWithQty = CreateItems();
            var invoice = invoiceSvc.CreateInvoice(itemsWithQty);

            DisplayInvoice(invoice);
            System.Console.ReadKey();
        }

        private static void CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(x => new LoggingInterceptor(System.Console.Out));
            builder.RegisterType<OrderCreatorService>().As<IOrderCreatorService>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<InvoiceCreatorService>().As<IInvoiceCreatorService>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));

            _container = builder.Build();
        }

        private static void DisplayInvoice(Invoice invoice)
        {
            System.Console.WriteLine($"Date: {invoice.Date}");
            System.Console.Write($"{"ITEM".PadRight(20)}");
            System.Console.Write($"{"QTY".PadRight(7)}");
            System.Console.Write($"{"PRICE".PadRight(7)}");
            System.Console.Write($"{"DISCOUNT".ToString().PadRight(14)}");
            System.Console.Write($"{"TAX".PadRight(14)}");
            System.Console.Write($"{"TOTAL".PadRight(7)}\n");
            System.Console.WriteLine($"=================================================");
            foreach (var o in invoice.Orders)
            {
                System.Console.Write($"{o.Item.Name.PadRight(20)}");
                System.Console.Write($"{o.Quantity.ToString().PadRight(7)}");
                System.Console.Write($"{o.Item.Price.ToString().PadRight(7)}");
                System.Console.Write($"%{(o.Discount * 100).ToString().PadRight(6)}");
                System.Console.Write($"{o.TotalDiscount.ToString().PadRight(7)}");
                System.Console.Write($"%{(o.Tax * 100).ToString().PadRight(6)}");
                System.Console.Write($"{o.TotalTax.ToString().PadRight(7)}");
                System.Console.Write($"{o.TotalPrice.ToString().PadRight(7)}\n");

            }
            System.Console.WriteLine($"TOTAL: {invoice.TotalPrice}");
        }
        
        private static List<ItemWithQuantity> CreateItems()
        {
            var bread = new Item("Bread", 10, Item.ItemType.Food);
            var milk = new Item("Milk", 5, Item.ItemType.Food);
            var toothpaste = new Item("Toothpaste", 15, Item.ItemType.Other);
            var toiletpaper = new Item("Toilet paper", 20, Item.ItemType.Other);

            var itemsWithQty = new List<ItemWithQuantity>
            {
                new ItemWithQuantity(bread, 1),
                new ItemWithQuantity(milk, 6),
                new ItemWithQuantity(toothpaste, 2),
                new ItemWithQuantity(toiletpaper, 20)
            };
            return itemsWithQty;
        }

        private static decimal DiscountCalculator(ItemWithQuantity currentItem, IEnumerable<ItemWithQuantity> allItems)
        {
            // milk - if buying 4, pay only for 3
            if(currentItem.Item.Name == "Milk" && currentItem.Quantity >= 4)
            {
                return (decimal)0.25;
            }

            // toothpaste - 10% on monday
            var toothpasteDay = DayOfWeek.Monday;
            if (currentItem.Item.Name == "Toothpaste" && DateTime.Now.DayOfWeek == toothpasteDay)
            {
                return (decimal)0.10;
            }

            // if buying more than 10 items with no discount, 15% off on all items with no other discounts
            var otherItems = allItems
                .Where(x =>
                    !(x.Item.Name == "Milk" && x.Quantity >= 4) &&
                    !(x.Item.Name == "Toothpaste" && DateTime.Now.DayOfWeek == toothpasteDay)).ToList();

            if (otherItems.Sum(x => x.Quantity) > 10)
            {
                return (decimal)0.15;
            }

            return 0;
        }
    }
}
