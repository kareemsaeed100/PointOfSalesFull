namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addthd_movToAcctransHed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccTransHeds", "thd_MovNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccTransHeds", "thd_MovNum");
        }
    }
}
