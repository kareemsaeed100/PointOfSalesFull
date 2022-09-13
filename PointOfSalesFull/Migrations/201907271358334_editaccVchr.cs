namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editaccVchr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccVchrs", "vch_cardnum", c => c.String());
            DropColumn("dbo.AccVchrs", "Unit_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccVchrs", "Unit_Name", c => c.String(nullable: false));
            AlterColumn("dbo.AccVchrs", "vch_cardnum", c => c.String(nullable: false));
        }
    }
}
