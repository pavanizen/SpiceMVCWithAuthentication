namespace Spicee.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMenuItemtoDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Spicyness = c.String(),
                        Image = c.String(),
                        CategoryId = c.Int(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId);
            
            AddForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.MenuItems", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.MenuItems", "CategoryId", "dbo.Categories");
            DropIndex("dbo.MenuItems", new[] { "SubCategoryId" });
            DropIndex("dbo.MenuItems", new[] { "CategoryId" });
            DropTable("dbo.MenuItems");
            AddForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
