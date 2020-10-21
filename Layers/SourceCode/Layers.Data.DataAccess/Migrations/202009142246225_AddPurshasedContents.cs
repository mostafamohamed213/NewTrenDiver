namespace Layers.Data.DataAccess.Migrations.Write
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPurshasedContents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchasedContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userid = c.Int(nullable: false),
                        ContentId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.userid, cascadeDelete: false)
                .Index(t => t.userid)
                .Index(t => t.ContentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchasedContents", "userid", "dbo.Users");
            DropForeignKey("dbo.PurchasedContents", "ContentId", "dbo.Contents");
            DropIndex("dbo.PurchasedContents", new[] { "ContentId" });
            DropIndex("dbo.PurchasedContents", new[] { "userid" });
            DropTable("dbo.PurchasedContents");
        }
    }
}
