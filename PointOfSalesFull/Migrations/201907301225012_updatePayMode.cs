namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePayMode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchasesInvoices", "Pay_ID1", c => c.Int(nullable: false));
            AddColumn("dbo.PurchasesInvoices", "Pay_ID2", c => c.Int(nullable: false));
            AddColumn("dbo.PurchasesInvoices", "Pay1Val", c => c.Single(nullable: false));
            AddColumn("dbo.PurchasesInvoices", "Pay2Val", c => c.Single(nullable: false));
            AddColumn("dbo.SalesInvoices", "Pay_ID1", c => c.Int(nullable: false));
            AddColumn("dbo.SalesInvoices", "Pay_ID2", c => c.Int(nullable: false));
            AddColumn("dbo.SalesInvoices", "Pay1Val", c => c.Single(nullable: false));
            AddColumn("dbo.SalesInvoices", "Pay2Val", c => c.Single(nullable: false));
            AddColumn("dbo.PurchasesReturns", "Pay_ID1", c => c.Int(nullable: false));
            AddColumn("dbo.PurchasesReturns", "Pay_ID2", c => c.Int(nullable: false));
            AddColumn("dbo.PurchasesReturns", "Pay1Val", c => c.Single(nullable: false));
            AddColumn("dbo.PurchasesReturns", "Pay2Val", c => c.Single(nullable: false));
            AddColumn("dbo.SalesReturns", "Pay_ID1", c => c.Int(nullable: false));
            AddColumn("dbo.SalesReturns", "Pay_ID2", c => c.Int(nullable: false));
            AddColumn("dbo.SalesReturns", "Pay1Val", c => c.Single(nullable: false));
            AddColumn("dbo.SalesReturns", "Pay2Val", c => c.Single(nullable: false));
            DropColumn("dbo.PurchasesInvoices", "Pay_ID");
            DropColumn("dbo.SalesInvoices", "Pay_ID");
            DropColumn("dbo.PurchasesReturns", "Pay_ID");
            DropColumn("dbo.SalesReturns", "Pay_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesReturns", "Pay_ID", c => c.Int(nullable: false));
            AddColumn("dbo.PurchasesReturns", "Pay_ID", c => c.Int(nullable: false));
            AddColumn("dbo.SalesInvoices", "Pay_ID", c => c.Int(nullable: false));
            AddColumn("dbo.PurchasesInvoices", "Pay_ID", c => c.Int(nullable: false));
            DropColumn("dbo.SalesReturns", "Pay2Val");
            DropColumn("dbo.SalesReturns", "Pay1Val");
            DropColumn("dbo.SalesReturns", "Pay_ID2");
            DropColumn("dbo.SalesReturns", "Pay_ID1");
            DropColumn("dbo.PurchasesReturns", "Pay2Val");
            DropColumn("dbo.PurchasesReturns", "Pay1Val");
            DropColumn("dbo.PurchasesReturns", "Pay_ID2");
            DropColumn("dbo.PurchasesReturns", "Pay_ID1");
            DropColumn("dbo.SalesInvoices", "Pay2Val");
            DropColumn("dbo.SalesInvoices", "Pay1Val");
            DropColumn("dbo.SalesInvoices", "Pay_ID2");
            DropColumn("dbo.SalesInvoices", "Pay_ID1");
            DropColumn("dbo.PurchasesInvoices", "Pay2Val");
            DropColumn("dbo.PurchasesInvoices", "Pay1Val");
            DropColumn("dbo.PurchasesInvoices", "Pay_ID2");
            DropColumn("dbo.PurchasesInvoices", "Pay_ID1");
        }
    }
}
