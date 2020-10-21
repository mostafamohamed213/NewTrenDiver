namespace Layers.Data.DataAccess.Migrations.Write
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCategoryLookup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LK_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniqueKey = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LK_Category_Localize",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LK_Language", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.LK_Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.LanguageId);
            
            AddColumn("dbo.Channel", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Channel", "CategoryId");
            AddForeignKey("dbo.Channel", "CategoryId", "dbo.LK_Category", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Channel", "CategoryId", "dbo.LK_Category");
            DropForeignKey("dbo.LK_Category_Localize", "CategoryId", "dbo.LK_Category");
            DropForeignKey("dbo.LK_Category_Localize", "LanguageId", "dbo.LK_Language");
            DropIndex("dbo.LK_Category_Localize", new[] { "LanguageId" });
            DropIndex("dbo.LK_Category_Localize", new[] { "CategoryId" });
            DropIndex("dbo.Channel", new[] { "CategoryId" });
            DropColumn("dbo.Channel", "CategoryId");
            DropTable("dbo.LK_Category_Localize");
            DropTable("dbo.LK_Category");
        }
    }
}
