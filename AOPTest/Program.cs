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
using System.IO;

namespace AOPTest
{
    class Program
    {
        private static IContainer _container;
        private static IInvoiceCreatorService _invoiceSvc;

        static void Main(string[] args)
        {
            CreateContainer();

            _invoiceSvc = _container.Resolve<IInvoiceCreatorService>();
            _invoiceSvc.DiscountCaclulator = DiscountCalculator.CalculateDiscount;

            while (true)
            {
                DisplayInvoices();

                Console.WriteLine("Press any key to create a new invoice or Escape to exit");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;

                CreateInvoice();
            }
        }

        private static void DisplayInvoices()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = _container.Resolve<IUnitOfWork>();
            
                var invoices = uow.Invoices.GetAll().Reverse();

                Console.Clear();
                Console.WriteLine("Invoices:");
                foreach (var i in invoices)
                {
                    DisplayInvoice(i);
                }
            }
        }

        private static void CreateInvoice()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var items = uow.Items.GetAll();
                var itemsWithQty = SelectItems(items);

                var invoice = _invoiceSvc.CreateInvoice(itemsWithQty);

                uow.Invoices.Add(invoice);
                uow.Save();
            }
        }

        private static IEnumerable<ItemWithQuantity> SelectItems(IEnumerable<Item> items)
        {
            List<ItemWithQuantity> selectedItems = new List<ItemWithQuantity>();
            while (true)
            {
                Console.Clear();
                ShowItems(items);
                Console.WriteLine("Select an item by id:");
                var input = Console.ReadLine();

                var item = items.SingleOrDefault(x => x.Id.ToString() == input);

                if (item != null)
                {
                    Console.WriteLine("How many?");
                    var qtyStr = Console.ReadLine();
                    var qty = int.Parse(qtyStr);

                    var index = selectedItems.FindIndex(x => x.Item.Id == item.Id);
                    if (index >= 0)
                    {
                        selectedItems[index] = new ItemWithQuantity(item, selectedItems[index].Quantity + qty);
                    }
                    else
                    {
                        selectedItems.Add(new ItemWithQuantity(item, qty));
                    }
                }


                Console.WriteLine("Press any key to select another item or Escape to exit");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;
            }

            return selectedItems;
        }

        private static void ShowItems(IEnumerable<Item> items)
        {
            Console.WriteLine("Items:");
            foreach (var item in items)
            {
                Console.Write(item.Id.ToString().PadRight(10));
                Console.Write(item.Name.PadRight(10));
                Console.Write(item.Price.ToString().PadRight(10) + "\n");
            }
        }

        private static void CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InvoicingContext>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceRepository>().As<IInvoiceRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ItemRepository>().As<IItemRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope().OwnedByLifetimeScope();
            builder.RegisterType<OrderCreatorService>().As<IOrderCreatorService>().SingleInstance();
            builder.RegisterType<InvoiceCreatorService>().As<IInvoiceCreatorService>().SingleInstance();

            _container = builder.Build();
        }


        public static void DisplayInvoice(Invoice invoice)
        {
            Console.WriteLine();
            Console.WriteLine($"==========================================================================");
            Console.WriteLine($"ID: {invoice.Id} Date: {invoice.Date}");
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
                Console.Write($"{o.ItemPrice.ToString("0.00").PadRight(7)}");
                Console.Write($"%{(o.Discount * 100).ToString("0").PadRight(6)}");
                Console.Write($"{o.TotalDiscount.ToString("0.00").PadRight(7)}");
                Console.Write($"%{(o.Tax * 100).ToString("0").PadRight(6)}");
                Console.Write($"{o.TotalTax.ToString("0.00").PadRight(7)}");
                Console.Write($"{o.TotalPrice.ToString("0.00").PadRight(7)}\n");

            }
            Console.WriteLine($"TOTAL: {invoice.TotalPrice.ToString("0.00")}");
            Console.WriteLine();
        }
    }
}
