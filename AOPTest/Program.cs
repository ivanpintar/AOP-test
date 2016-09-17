using AOPTest.Domain.Services;
using System.Collections.Generic;
using System;
using System.Linq;
using Autofac;
using Autofac.Extras.DynamicProxy;
using AOPTest.Data.Repositories;
using AOPTest.Data;
using AOPTest.Domain.Entities;
using AOPTest.AOP;

namespace AOPTest
{
    class Program
    {
        private static IContainer _container;

        static void Main(string[] args)
        {
            CreateContainer();

            var invoiceSvc = _container.Resolve<IInvoiceCreatorService>();
            invoiceSvc.DiscountCaclulator = DiscountCalculator.CalculateDiscount;

            var itemsWithQty = GetItems();
            var invoice = invoiceSvc.CreateInvoice(itemsWithQty);

            DisplayInvoice(invoice);
            Console.ReadKey();
        }

        private static void CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InvoicingContext>();
            builder.RegisterType<InvoiceRepository>().As<IInvoiceRepository>();
            builder.RegisterType<ItemRepository>().As<IItemRepository>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<OrderCreatorService>().As<IOrderCreatorService>();
            builder.RegisterType<InvoiceCreatorService>().As<IInvoiceCreatorService>();

            _container = builder.Build();
        }

        private static List<ItemWithQuantity> GetItems()
        {
            using (var uow = _container.Resolve<IUnitOfWork>())
            {
                var items = uow.Items.GetAll();
                var i = 1;
                return items.Select(x => new ItemWithQuantity(x, i++)).ToList();
            }
        }

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
