using System.Collections.Generic;
using AOPTest.Domain.Entities;

namespace AOPTest.Data.Repositories
{
    public interface IItemRepository
    {
        void Add(Item item);
        IEnumerable<Item> GetAll();
    }
}