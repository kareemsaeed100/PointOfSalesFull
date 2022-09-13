namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAccStocTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccStocks", "ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.AccStocks", "ProductName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccStocks", "ProductName", c => c.String(nullable: false));
            DropColumn("dbo.AccStocks", "ProductId");
        }
    }
}
