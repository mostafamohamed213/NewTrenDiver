namespace Layers.Data.DataAccess.Migrations.Read
{
    using Layers.Data.DataAccess.Migrations.Shared;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlanguageview : DbMigration
    {
        public override void Up()
        {
            string script = ScriptReader.Read(MigrationContext.Read, "VW_LanguageLookup_Create");
            Sql(script);

        }
        
        public override void Down()
        {
            string script = ScriptReader.Read(MigrationContext.Read, "VW_LanguageLookup_DROP");
            Sql(script);
        }
    }
}
