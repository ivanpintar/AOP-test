using AOPTest.AOP;
using AOPTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Data.Repositories
{
    public class ItemRepository : IDisposable, IItemRepository
    {
        private InvoicingContext _ctx;

        public ItemRepository(InvoicingContext ctx)
        {
            _ctx = ctx;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        [LoggingInterceptor]
        public IEnumerable<Item> GetAll()
        {
            return _ctx.Items.ToList();
        }

        [LoggingInterceptor]
        public void AddItem(Item item)
        {
            _ctx.Items.Add(item);
        }
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
