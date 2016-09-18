using System.Collections.Generic;
using AOPTest.Domain.Entities;

namespace AOPTest.Data.Repositories
{
    public interface IInvoiceRepository
    {
        void Add(Invoice invoice);
        IEnumerable<Invoice> GetAll();
    }
}