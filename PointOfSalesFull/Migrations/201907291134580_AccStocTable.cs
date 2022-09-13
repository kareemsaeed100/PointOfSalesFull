namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccStocTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccStocks",
                c => new
                    {
                        Stock_ID = c.Int(nullable: false, identity: true),
                        MovDate = c.DateTime(nullable: false),
                        ProductName = c.String(nullable: false),
                        Quintity = c.Int(nullable: false),
                        MoveType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Stock_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccStocks");
        }
    }
}
