namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDefinationTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccTransDets", "tdt_lne", c => c.String(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_typ", c => c.String(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_L1", c => c.String(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_L2", c => c.String(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_C1", c => c.String(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_cr", c => c.Single(nullable: false));
            AlterColumn("dbo.AccTransHeds", "thd_typ", c => c.String(nullable: false));
            AlterColumn("dbo.AccVchrs", "vch_typ", c => c.String());
            AlterColumn("dbo.Definations", "PureAcc", c => c.String());
            AlterColumn("dbo.Definations", "PureRAcc", c => c.String());
            AlterColumn("dbo.Definations", "SalesAcc", c => c.String());
            AlterColumn("dbo.Definations", "SalesRAcc", c => c.String());
            AlterColumn("dbo.Definations", "ReciptAcc", c => c.String());
            AlterColumn("dbo.Definations", "PayMentAcc", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Definations", "PayMentAcc", c => c.Int(nullable: false));
            AlterColumn("dbo.Definations", "ReciptAcc", c => c.Int(nullable: false));
            AlterColumn("dbo.Definations", "SalesRAcc", c => c.Int(nullable: false));
            AlterColumn("dbo.Definations", "SalesAcc", c => c.Int(nullable: false));
            AlterColumn("dbo.Definations", "PureRAcc", c => c.Int(nullable: false));
            AlterColumn("dbo.Definations", "PureAcc", c => c.Int(nullable: false));
            AlterColumn("dbo.AccVchrs", "vch_typ", c => c.Int(nullable: false));
            AlterColumn("dbo.AccTransHeds", "thd_typ", c => c.Int(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_cr", c => c.Int(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_C1", c => c.Single(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_L2", c => c.Int(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_L1", c => c.Int(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_typ", c => c.Int(nullable: false));
            AlterColumn("dbo.AccTransDets", "tdt_lne", c => c.Int(nullable: false));
        }
    }
}
