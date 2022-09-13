namespace PointOfSalesFull.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccTransDets",
                c => new
                    {
                        tdt_Key = c.Int(nullable: false, identity: true),
                        thd_num = c.Int(nullable: false),
                        tdt_num = c.Int(nullable: false),
                        tdt_lne = c.Int(nullable: false),
                        tdt_typ = c.Int(nullable: false),
                        tdt_brn = c.Int(nullable: false),
                        tdt_L1 = c.Int(nullable: false),
                        tdt_L2 = c.Int(nullable: false),
                        tdt_C1 = c.Single(nullable: false),
                        tdt_dr = c.Single(nullable: false),
                        tdt_cr = c.Int(nullable: false),
                        tdt_dat = c.DateTime(nullable: false),
                        tdt_des = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.tdt_Key)
                .ForeignKey("dbo.AccTransHeds", t => t.thd_num, cascadeDelete: true)
                .Index(t => t.thd_num);
            
            CreateTable(
                "dbo.AccTransHeds",
                c => new
                    {
                        thd_Key = c.Int(nullable: false, identity: true),
                        thd_num = c.Int(nullable: false),
                        thd_typ = c.Int(nullable: false),
                        thd_brn = c.Int(nullable: false),
                        thd_dat = c.DateTime(nullable: false),
                        thd_des = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.thd_Key);
            
            CreateTable(
                "dbo.AccVchrs",
                c => new
                    {
                        vch_num = c.Int(nullable: false, identity: true),
                        vch_Brn = c.Int(nullable: false),
                        Unit_Name = c.String(nullable: false),
                        vch_DatG = c.DateTime(nullable: false),
                        vch_typ = c.Int(nullable: false),
                        vch_paymod = c.Int(nullable: false),
                        vch_Val = c.Single(nullable: false),
                        vch_cardnum = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.vch_num);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Branch_ID = c.Int(nullable: false, identity: true),
                        Branch_Name = c.String(nullable: false),
                        Branch_EName = c.String(),
                    })
                .PrimaryKey(t => t.Branch_ID);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Brand_ID = c.Int(nullable: false, identity: true),
                        Brand_Name = c.String(nullable: false),
                        Brand_EName = c.String(),
                    })
                .PrimaryKey(t => t.Brand_ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Cat_ID = c.Int(nullable: false, identity: true),
                        Cat_Descrption = c.String(nullable: false),
                        Cat_EDescrption = c.String(),
                        Dep_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Cat_ID)
                .ForeignKey("dbo.Departments", t => t.Dep_ID, cascadeDelete: true)
                .Index(t => t.Dep_ID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Deb_ID = c.Int(nullable: false, identity: true),
                        Deb_Name = c.String(nullable: false),
                        Deb_EName = c.String(),
                    })
                .PrimaryKey(t => t.Deb_ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Product_ID = c.String(nullable: false, maxLength: 128),
                        Product_Name = c.String(nullable: false),
                        Quintity = c.Int(),
                        Price = c.Single(nullable: false),
                        Image = c.String(),
                        Cat_ID = c.Int(nullable: false),
                        Brand_Id = c.Int(nullable: false),
                        Uint_Id = c.Int(nullable: false),
                        For_Sale = c.Int(nullable: false),
                        Tax_State = c.Int(nullable: false),
                        Return = c.Int(nullable: false),
                        Mov_State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Product_ID)
                .ForeignKey("dbo.Categories", t => t.Cat_ID, cascadeDelete: true)
                .Index(t => t.Cat_ID);
            
            CreateTable(
                "dbo.PurchasesDetailes",
                c => new
                    {
                        Pure_Det_Key = c.Int(nullable: false, identity: true),
                        Pure_ID = c.Int(nullable: false),
                        Product_ID = c.String(maxLength: 128),
                        Quintity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Amount = c.Single(nullable: false),
                        discount = c.Single(nullable: false),
                        Tax = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Pure_Det_Key)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .ForeignKey("dbo.PurchasesInvoices", t => t.Pure_ID, cascadeDelete: true)
                .Index(t => t.Pure_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.PurchasesInvoices",
                c => new
                    {
                        Pure_ID = c.Int(nullable: false, identity: true),
                        Pay_ID = c.Int(nullable: false),
                        Brn_Num = c.Int(nullable: false),
                        Pure_Date = c.DateTime(),
                        Sub_ID = c.Int(),
                        Saller_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Pure_ID)
                .ForeignKey("dbo.Sallers", t => t.Saller_ID)
                .ForeignKey("dbo.Suppliers", t => t.Sub_ID)
                .Index(t => t.Sub_ID)
                .Index(t => t.Saller_ID);
            
            CreateTable(
                "dbo.Sallers",
                c => new
                    {
                        Saller_ID = c.Int(nullable: false, identity: true),
                        Saler_Name = c.String(nullable: false),
                        Saler_EName = c.String(),
                        Saler_Email = c.String(),
                        Saler_Phon = c.String(),
                        Saler_Adress = c.String(),
                        Nat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Saller_ID);
            
            CreateTable(
                "dbo.SalesInvoices",
                c => new
                    {
                        Sales_ID = c.Int(nullable: false, identity: true),
                        Sales_Date = c.DateTime(),
                        Customer_ID = c.Int(),
                        Saller_ID = c.Int(),
                        Pay_ID = c.Int(nullable: false),
                        Brn_Num = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sales_ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID)
                .ForeignKey("dbo.Sallers", t => t.Saller_ID)
                .Index(t => t.Customer_ID)
                .Index(t => t.Saller_ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Customes_ID = c.Int(nullable: false, identity: true),
                        First_Name = c.String(nullable: false),
                        Nat_ID = c.Int(nullable: false),
                        Last_Name = c.String(),
                        First_EName = c.String(),
                        Last_EName = c.String(),
                        Phon = c.String(),
                        Email = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Customes_ID);
            
            CreateTable(
                "dbo.Sales_Detailes",
                c => new
                    {
                        Sales_Det_Key = c.Int(nullable: false, identity: true),
                        Sales_ID = c.Int(nullable: false),
                        Product_ID = c.String(maxLength: 128),
                        Quintity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Amount = c.Single(nullable: false),
                        discount = c.Single(nullable: false),
                        Tax = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Sales_Det_Key)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .ForeignKey("dbo.SalesInvoices", t => t.Sales_ID, cascadeDelete: true)
                .Index(t => t.Sales_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Sub_ID = c.Int(nullable: false, identity: true),
                        Sub_Name = c.String(nullable: false),
                        Sub_EName = c.String(),
                        Sub_Email = c.String(),
                        Sub_Phon = c.String(),
                        Sub_Adress = c.String(),
                        Nat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sub_ID);
            
            CreateTable(
                "dbo.PurchasesReturnDetailes",
                c => new
                    {
                        PureR_Det_Key = c.Int(nullable: false, identity: true),
                        PureR_ID = c.Int(nullable: false),
                        Product_ID = c.String(maxLength: 128),
                        Quintity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Amount = c.Single(nullable: false),
                        discount = c.Single(nullable: false),
                        Tax = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.PureR_Det_Key)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .ForeignKey("dbo.PurchasesReturns", t => t.PureR_ID, cascadeDelete: true)
                .Index(t => t.PureR_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.PurchasesReturns",
                c => new
                    {
                        PureR_ID = c.Int(nullable: false, identity: true),
                        Pay_ID = c.Int(nullable: false),
                        Brn_Num = c.Int(nullable: false),
                        PureR_Date = c.DateTime(),
                        Sub_ID = c.Int(),
                        Saller_ID = c.Int(),
                    })
                .PrimaryKey(t => t.PureR_ID)
                .ForeignKey("dbo.Sallers", t => t.Saller_ID)
                .ForeignKey("dbo.Suppliers", t => t.Sub_ID)
                .Index(t => t.Sub_ID)
                .Index(t => t.Saller_ID);
            
            CreateTable(
                "dbo.SalesReturnDetailes",
                c => new
                    {
                        SalesR_Det_Key = c.Int(nullable: false, identity: true),
                        SalesR_ID = c.Int(nullable: false),
                        Product_ID = c.String(maxLength: 128),
                        Quintity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Amount = c.Single(nullable: false),
                        discount = c.Single(nullable: false),
                        Tax = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.SalesR_Det_Key)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .ForeignKey("dbo.SalesReturns", t => t.SalesR_ID, cascadeDelete: true)
                .Index(t => t.SalesR_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.SalesReturns",
                c => new
                    {
                        SalesR_ID = c.Int(nullable: false, identity: true),
                        SalesR_Date = c.DateTime(),
                        Customer_ID = c.Int(),
                        Saller_ID = c.Int(),
                        Pay_ID = c.Int(nullable: false),
                        Brn_Num = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalesR_ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID)
                .ForeignKey("dbo.Sallers", t => t.Saller_ID)
                .Index(t => t.Customer_ID)
                .Index(t => t.Saller_ID);
            
            CreateTable(
                "dbo.Definations",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        PureAcc = c.Int(nullable: false),
                        PureRAcc = c.Int(nullable: false),
                        SalesAcc = c.Int(nullable: false),
                        SalesRAcc = c.Int(nullable: false),
                        ReciptAcc = c.Int(nullable: false),
                        PayMentAcc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.Nationalities",
                c => new
                    {
                        Nat_ID = c.Int(nullable: false, identity: true),
                        Nat_Name = c.String(nullable: false),
                        Nat_EName = c.String(),
                    })
                .PrimaryKey(t => t.Nat_ID);
            
            CreateTable(
                "dbo.PayModes",
                c => new
                    {
                        Pay_ID = c.Int(nullable: false, identity: true),
                        Pay_Name = c.String(nullable: false),
                        Pay_EName = c.String(nullable: false),
                        Pay_Acc = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Pay_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        RolDescrption = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Stock_ID = c.Int(nullable: false, identity: true),
                        Stock_Name = c.String(nullable: false),
                        Prod_Id = c.Int(nullable: false),
                        Prod_Quintity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Stock_ID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Unit_ID = c.Int(nullable: false, identity: true),
                        Unit_Name = c.String(nullable: false),
                        Unit_EName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Unit_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SalesReturns", "Saller_ID", "dbo.Sallers");
            DropForeignKey("dbo.SalesReturnDetailes", "SalesR_ID", "dbo.SalesReturns");
            DropForeignKey("dbo.SalesReturns", "Customer_ID", "dbo.Customers");
            DropForeignKey("dbo.SalesReturnDetailes", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.PurchasesReturns", "Sub_ID", "dbo.Suppliers");
            DropForeignKey("dbo.PurchasesReturns", "Saller_ID", "dbo.Sallers");
            DropForeignKey("dbo.PurchasesReturnDetailes", "PureR_ID", "dbo.PurchasesReturns");
            DropForeignKey("dbo.PurchasesReturnDetailes", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.PurchasesInvoices", "Sub_ID", "dbo.Suppliers");
            DropForeignKey("dbo.SalesInvoices", "Saller_ID", "dbo.Sallers");
            DropForeignKey("dbo.Sales_Detailes", "Sales_ID", "dbo.SalesInvoices");
            DropForeignKey("dbo.Sales_Detailes", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.SalesInvoices", "Customer_ID", "dbo.Customers");
            DropForeignKey("dbo.PurchasesInvoices", "Saller_ID", "dbo.Sallers");
            DropForeignKey("dbo.PurchasesDetailes", "Pure_ID", "dbo.PurchasesInvoices");
            DropForeignKey("dbo.PurchasesDetailes", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Products", "Cat_ID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Dep_ID", "dbo.Departments");
            DropForeignKey("dbo.AccTransDets", "thd_num", "dbo.AccTransHeds");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SalesReturns", new[] { "Saller_ID" });
            DropIndex("dbo.SalesReturns", new[] { "Customer_ID" });
            DropIndex("dbo.SalesReturnDetailes", new[] { "Product_ID" });
            DropIndex("dbo.SalesReturnDetailes", new[] { "SalesR_ID" });
            DropIndex("dbo.PurchasesReturns", new[] { "Saller_ID" });
            DropIndex("dbo.PurchasesReturns", new[] { "Sub_ID" });
            DropIndex("dbo.PurchasesReturnDetailes", new[] { "Product_ID" });
            DropIndex("dbo.PurchasesReturnDetailes", new[] { "PureR_ID" });
            DropIndex("dbo.Sales_Detailes", new[] { "Product_ID" });
            DropIndex("dbo.Sales_Detailes", new[] { "Sales_ID" });
            DropIndex("dbo.SalesInvoices", new[] { "Saller_ID" });
            DropIndex("dbo.SalesInvoices", new[] { "Customer_ID" });
            DropIndex("dbo.PurchasesInvoices", new[] { "Saller_ID" });
            DropIndex("dbo.PurchasesInvoices", new[] { "Sub_ID" });
            DropIndex("dbo.PurchasesDetailes", new[] { "Product_ID" });
            DropIndex("dbo.PurchasesDetailes", new[] { "Pure_ID" });
            DropIndex("dbo.Products", new[] { "Cat_ID" });
            DropIndex("dbo.Categories", new[] { "Dep_ID" });
            DropIndex("dbo.AccTransDets", new[] { "thd_num" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Units");
            DropTable("dbo.Stocks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PayModes");
            DropTable("dbo.Nationalities");
            DropTable("dbo.Definations");
            DropTable("dbo.SalesReturns");
            DropTable("dbo.SalesReturnDetailes");
            DropTable("dbo.PurchasesReturns");
            DropTable("dbo.PurchasesReturnDetailes");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Sales_Detailes");
            DropTable("dbo.Customers");
            DropTable("dbo.SalesInvoices");
            DropTable("dbo.Sallers");
            DropTable("dbo.PurchasesInvoices");
            DropTable("dbo.PurchasesDetailes");
            DropTable("dbo.Products");
            DropTable("dbo.Departments");
            DropTable("dbo.Categories");
            DropTable("dbo.Brands");
            DropTable("dbo.Branches");
            DropTable("dbo.AccVchrs");
            DropTable("dbo.AccTransHeds");
            DropTable("dbo.AccTransDets");
        }
    }
}
