namespace AOPTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCalculatedPropsFromOrder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Order", "TotalPrice");
            DropColumn("dbo.Order", "TotalTax");
            DropColumn("dbo.Order", "TotalDiscount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "TotalDiscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Order", "TotalTax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Order", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
