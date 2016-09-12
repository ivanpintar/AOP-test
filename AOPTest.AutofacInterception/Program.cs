using AOPTest.Domain.Entities;
using AOPTest.Domain.Services;
using System.Collections.Generic;
using System;
using System.Linq;
using Autofac;
using Autofac.Extras.DynamicProxy;
using AOPTest.AutofacInterception.AOP;
using AOPTest.Data.Repositories;
using AOPTest.Data;
using AOPTest.Helpers;

namespace AOPTest.AutofacInterception
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

            HelperMethods.DisplayInvoice(invoice);
            Console.ReadKey();
        }

        private static void CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(x => new LoggingInterceptor(System.Console.Out));
            builder.RegisterType<InvoicingContext>();
            builder.RegisterType<InvoiceRepository>().As<IInvoiceRepository>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<ItemRepository>().As<IItemRepository>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<OrderCreatorService>().As<IOrderCreatorService>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<InvoiceCreatorService>().As<IInvoiceCreatorService>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));

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
    }
}
