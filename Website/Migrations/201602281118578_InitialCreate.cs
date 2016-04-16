namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.StockItem",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        AvailableQuantity = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("dbo.Category", t => t.Category_CategoryId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.StockItem", "Category_CategoryId", "dbo.Category");
            DropIndex("dbo.StockItem", new[] { "Category_CategoryId" });
            DropIndex("dbo.StockItem", new[] { "OrderId" });
            DropTable("dbo.User");
            DropTable("dbo.StockItem");
            DropTable("dbo.Order");
            DropTable("dbo.Category");
        }
    }
}
