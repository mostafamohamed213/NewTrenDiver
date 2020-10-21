namespace Layers.Data.DataAccess.Migrations.Read
{
    using Layers.Data.DataAccess.Migrations.Shared;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcontentTargetViewers : DbMigration
    {
        public override void Up()
        {


            string script = ScriptReader.Read(MigrationContext.Read, "VW_CreateContentTargetviewers");
            Sql(script);

        }
        
        public override void Down()
        {

            string script = ScriptReader.Read(MigrationContext.Read, "VW_DropContentTargetViewers");
            Sql(script);
        }
    }
}
