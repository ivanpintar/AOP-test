namespace AOPTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrdersToInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Invoice_Id", c => c.Int());
            CreateIndex("dbo.Order", "Invoice_Id");
            AddForeignKey("dbo.Order", "Invoice_Id", "dbo.Invoice", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "Invoice_Id", "dbo.Invoice");
            DropIndex("dbo.Order", new[] { "Invoice_Id" });
            DropColumn("dbo.Order", "Invoice_Id");
        }
    }
}
