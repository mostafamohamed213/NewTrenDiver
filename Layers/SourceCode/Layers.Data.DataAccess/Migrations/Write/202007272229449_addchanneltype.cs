namespace Layers.Data.DataAccess.Migrations.Write
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addchanneltype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Channel", "channelType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Channel", "channelType");
        }
    }
}
