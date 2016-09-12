using System.Collections.Generic;
using AOPTest.Domain.Entities;

namespace AOPTest.Data.Repositories
{
    public interface IItemRepository
    {
        void AddItem(Item item);
        void Dispose();
        IEnumerable<Item> GetAll();
    }
}