using AOPTest.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AOPTest.Data
{
    public class InvoicingContext : DbContext
    {
        public InvoicingContext() : base("InvoicingContext")
        {           
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Invoice>().HasMany(x => x.Orders);
            modelBuilder.Entity<Invoice>().Ignore(x => x.TotalPrice);
            modelBuilder.Entity<Order>().Ignore(x => x.TotalDiscount);
            modelBuilder.Entity<Order>().Ignore(x => x.TotalTax);
            modelBuilder.Entity<Order>().Ignore(x => x.TotalPrice);
        }
    }
}
