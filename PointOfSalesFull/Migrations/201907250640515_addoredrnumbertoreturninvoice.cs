namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addoredrnumbertoreturninvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchasesReturns", "Pure_Number", c => c.Int(nullable: false));
            AddColumn("dbo.SalesReturns", "Sales_Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalesReturns", "Sales_Number");
            DropColumn("dbo.PurchasesReturns", "Pure_Number");
        }
    }
}
