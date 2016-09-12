using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private InvoicingContext _ctx;

        public IItemRepository Items { get; private set; }
        public IInvoiceRepository Invoices { get; private set; }

        public UnitOfWork(InvoicingContext ctx, IItemRepository itemRepo, IInvoiceRepository invoiceRepo)
        {
            _ctx = ctx;

            Items = itemRepo;
            Invoices = invoiceRepo;
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
        
        public void Dispose()
        {
            _ctx.Dispose();
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
