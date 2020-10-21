namespace Layers.Data.DataAccess.Migrations.Read
{
    using Layers.Data.DataAccess.Migrations.Shared;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcontentview : DbMigration
    {
        public override void Up()
        {
            string script = ScriptReader.Read(MigrationContext.Read, "VWContent_Create");
            Sql(script);

        }
        
        public override void Down()
        {
            string script = ScriptReader.Read(MigrationContext.Read, "VWContent_Drop");
            Sql(script);
        }
    }
}
