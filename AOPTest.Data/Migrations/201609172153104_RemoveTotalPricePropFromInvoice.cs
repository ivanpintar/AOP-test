namespace AOPTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTotalPricePropFromInvoice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Invoice", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoice", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
