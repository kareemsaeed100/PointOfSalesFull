namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateaccvchaddsubandpurnum2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccVchrs", "vch_paymod1", c => c.Int(nullable: false));
            AddColumn("dbo.AccVchrs", "vch_paymod2", c => c.Int(nullable: false));
            AddColumn("dbo.AccVchrs", "vch_Paid1", c => c.Single(nullable: false));
            AddColumn("dbo.AccVchrs", "vch_Paid2", c => c.Single(nullable: false));
            DropColumn("dbo.AccVchrs", "vch_paymod");
            DropColumn("dbo.AccVchrs", "vch_Paid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccVchrs", "vch_Paid", c => c.Single(nullable: false));
            AddColumn("dbo.AccVchrs", "vch_paymod", c => c.Int(nullable: false));
            DropColumn("dbo.AccVchrs", "vch_Paid2");
            DropColumn("dbo.AccVchrs", "vch_Paid1");
            DropColumn("dbo.AccVchrs", "vch_paymod2");
            DropColumn("dbo.AccVchrs", "vch_paymod1");
        }
    }
}
