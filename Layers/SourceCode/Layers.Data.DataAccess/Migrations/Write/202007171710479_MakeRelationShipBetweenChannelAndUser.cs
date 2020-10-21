namespace Layers.Data.DataAccess.Migrations.Write
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeRelationShipBetweenChannelAndUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "Published", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contents", "Price", c => c.Single(nullable: false));
            AddColumn("dbo.Channel", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UserType", c => c.Int(nullable: false));
            CreateIndex("dbo.Channel", "UserId");
            AddForeignKey("dbo.Channel", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Channel", "UserId", "dbo.Users");
            DropIndex("dbo.Channel", new[] { "UserId" });
            DropColumn("dbo.Users", "UserType");
            DropColumn("dbo.Channel", "UserId");
            DropColumn("dbo.Contents", "Price");
            DropColumn("dbo.Contents", "Published");
        }
    }
}
