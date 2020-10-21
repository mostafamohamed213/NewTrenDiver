namespace Layers.Data.DataAccess.Migrations.Write
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LanguageTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LK_Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Abbreviation = c.String(),
                        UniqueKey = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LK_Language_Local",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageId = c.Int(nullable: false),
                        LanguageId1 = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LK_Language", t => t.LanguageId1, cascadeDelete: true)
                .Index(t => t.LanguageId1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LK_Language_Local", "LanguageId1", "dbo.LK_Language");
            DropIndex("dbo.LK_Language_Local", new[] { "LanguageId1" });
            DropTable("dbo.LK_Language_Local");
            DropTable("dbo.LK_Language");
        }
    }
}
