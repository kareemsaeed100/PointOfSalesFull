namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDefinationTable3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccTransDets", "tdt_lne", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccTransDets", "tdt_lne", c => c.Int(nullable: false));
        }
    }
}
