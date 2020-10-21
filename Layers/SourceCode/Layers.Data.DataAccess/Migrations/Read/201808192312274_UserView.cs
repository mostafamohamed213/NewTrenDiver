namespace Layers.Data.DataAccess.Migrations.Read
{
    using Shared;
    using System;
    using System.Data.Entity.Migrations;

    public partial class UserView : DbMigration
    {
        public override void Up()
        {
            string script = ScriptReader.Read(MigrationContext.Read, "VW_User_Create");
            Sql(script);
        }
        
        public override void Down()
        {
            string script = ScriptReader.Read(MigrationContext.Read, "VW_User_Drop");
            Sql(script);
        }
    }
}
