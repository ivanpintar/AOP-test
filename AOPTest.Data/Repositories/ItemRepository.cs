using AOPTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTest.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private InvoicingContext _ctx;

        public ItemRepository(InvoicingContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Item> GetAll()
        {
            return _ctx.Items.ToList();
        }

        public void Add(Item item)
        {
            _ctx.Items.Add(item);
        }
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
