using AOPTest.AOP;
using AOPTest.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AOPTest.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private InvoicingContext _ctx;

        public InvoiceRepository(InvoicingContext ctx)
        {
            _ctx = ctx;
        }

        [LoggingInterceptor]
        public IEnumerable<Invoice> GetAll()
        {
            return _ctx.Invoices.ToList();
        }

        [LoggingInterceptor]
        public void Add(Invoice invoice)
        {
            _ctx.Invoices.Add(invoice);
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}