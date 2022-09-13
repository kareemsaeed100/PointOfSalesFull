namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updataccvchr123 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AccVchrs", "vch_typ");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccVchrs", "vch_typ", c => c.String());
        }
    }
}
