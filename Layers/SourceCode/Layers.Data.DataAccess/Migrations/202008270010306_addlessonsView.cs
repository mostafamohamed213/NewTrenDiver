namespace Layers.Data.DataAccess.Migrations.Read
{
    using Layers.Data.DataAccess.Migrations.Shared;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlessonsView : DbMigration
    {
        public override void Up()
        {
            string script = ScriptReader.Read(MigrationContext.Read, "VW_Lessons_Create");
            Sql(script);

        }
        
        public override void Down()
        {
            string script = ScriptReader.Read(MigrationContext.Read, "VW_Lessons_Drop");
            Sql(script);

        }
    }
}
