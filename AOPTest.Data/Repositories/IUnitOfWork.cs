using System;

namespace AOPTest.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IInvoiceRepository Invoices { get; }
        IItemRepository Items { get; }

        void Dispose();
        void Save();
    }
}