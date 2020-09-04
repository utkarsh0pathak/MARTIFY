namespace TSSMARTIFYOnlineMart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeMartifyDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        BillDate = c.DateTime(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        PaymentModeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: false)
                .ForeignKey("dbo.PaymentModes", t => t.PaymentModeID, cascadeDelete:false)
                .Index(t => t.CustomerID)
                .Index(t => t.PaymentModeID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 30),
                        LoginTypeID = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PinCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.LoginTypes", t => t.LoginTypeID, cascadeDelete: false)
                .Index(t => t.LoginTypeID);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        PurchaseQTY = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: false)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        ProductName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ProductQTY = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        ProductImage = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: false)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: false)
                .Index(t => t.CustomerID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.TransactionDetails",
                c => new
                    {
                        TrasactionID = c.Int(nullable: false, identity: true),
                        BillID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        PruchaseQTY = c.Int(nullable: false),
                        PurchaseAmout = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TrasactionID)
                .ForeignKey("dbo.Bills", t => t.BillID, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: false)
                .Index(t => t.BillID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.LoginTypes",
                c => new
                    {
                        LoginTypeID = c.Int(nullable: false, identity: true),
                        LoginTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LoginTypeID);
            
            CreateTable(
                "dbo.PaymentModes",
                c => new
                    {
                        PaymentModeID = c.Int(nullable: false, identity: true),
                        PaymentModeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentModeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "PaymentModeID", "dbo.PaymentModes");
            DropForeignKey("dbo.Bills", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Customers", "LoginTypeID", "dbo.LoginTypes");
            DropForeignKey("dbo.Carts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.TransactionDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.TransactionDetails", "BillID", "dbo.Bills");
            DropForeignKey("dbo.Products", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Carts", "CustomerID", "dbo.Customers");
            DropIndex("dbo.TransactionDetails", new[] { "ProductID" });
            DropIndex("dbo.TransactionDetails", new[] { "BillID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "CustomerID" });
            DropIndex("dbo.Carts", new[] { "ProductID" });
            DropIndex("dbo.Carts", new[] { "CustomerID" });
            DropIndex("dbo.Customers", new[] { "LoginTypeID" });
            DropIndex("dbo.Bills", new[] { "PaymentModeID" });
            DropIndex("dbo.Bills", new[] { "CustomerID" });
            DropTable("dbo.PaymentModes");
            DropTable("dbo.LoginTypes");
            DropTable("dbo.TransactionDetails");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
            DropTable("dbo.Customers");
            DropTable("dbo.Bills");
        }
    }
}
