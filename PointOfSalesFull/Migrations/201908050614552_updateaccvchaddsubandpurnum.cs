namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateaccvchaddsubandpurnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccVchrs", "Vch_PurNum", c => c.Int(nullable: false));
            AddColumn("dbo.AccVchrs", "Vch_SubNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccVchrs", "Vch_SubNum");
            DropColumn("dbo.AccVchrs", "Vch_PurNum");
        }
    }
}
