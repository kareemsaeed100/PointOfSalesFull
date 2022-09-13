namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDefinationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Definations", "AccCustomer", c => c.String());
            AddColumn("dbo.Definations", "AccCash", c => c.String());
            AddColumn("dbo.Definations", "AccSalesRevenues", c => c.String());
            AddColumn("dbo.Definations", "AccTax", c => c.String());
            AddColumn("dbo.Definations", "AccSalesCost", c => c.String());
            AddColumn("dbo.Definations", "AccInv", c => c.String());
            AddColumn("dbo.Definations", "AccBank", c => c.String());
            AddColumn("dbo.Definations", "AccSublier", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Definations", "AccSublier");
            DropColumn("dbo.Definations", "AccBank");
            DropColumn("dbo.Definations", "AccInv");
            DropColumn("dbo.Definations", "AccSalesCost");
            DropColumn("dbo.Definations", "AccTax");
            DropColumn("dbo.Definations", "AccSalesRevenues");
            DropColumn("dbo.Definations", "AccCash");
            DropColumn("dbo.Definations", "AccCustomer");
        }
    }
}
