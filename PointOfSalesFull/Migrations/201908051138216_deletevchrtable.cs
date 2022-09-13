namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletevchrtable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Vchrs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Vchrs",
                c => new
                    {
                        vch_num = c.Int(nullable: false, identity: true),
                        vch_Brn = c.Int(nullable: false),
                        vch_DatG = c.DateTime(nullable: false),
                        vch_paymod1 = c.Int(nullable: false),
                        vch_paymod2 = c.Int(nullable: false),
                        Vch_PurNum = c.Int(nullable: false),
                        Vch_SubNum = c.Int(nullable: false),
                        vch_cardnum = c.String(),
                        vch_Paid1 = c.Single(nullable: false),
                        vch_Paid2 = c.Single(nullable: false),
                        vch_Wanted = c.Single(nullable: false),
                        vch_Remind = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.vch_num);
            
        }
    }
}
