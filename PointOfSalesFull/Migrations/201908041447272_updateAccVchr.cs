namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAccVchr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccVchrs", "vch_Paid", c => c.Single(nullable: false));
            AddColumn("dbo.AccVchrs", "vch_Wanted", c => c.Single(nullable: false));
            AddColumn("dbo.AccVchrs", "vch_Remind", c => c.Single(nullable: false));
            DropColumn("dbo.AccVchrs", "vch_Val");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccVchrs", "vch_Val", c => c.Single(nullable: false));
            DropColumn("dbo.AccVchrs", "vch_Remind");
            DropColumn("dbo.AccVchrs", "vch_Wanted");
            DropColumn("dbo.AccVchrs", "vch_Paid");
        }
    }
}
