namespace Layers.Data.DataAccess.Migrations.Read
{
    using Layers.Data.DataAccess.Migrations.Shared;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createlookupview : DbMigration
    {
        public override void Up()
        {
            
           string script = ScriptReader.Read(MigrationContext.Read, "VW_Create_Categorylookup");
            Sql(script);

        }
        
        public override void Down()
        {
         
       string script = ScriptReader.Read(MigrationContext.Read, " VW_Drop_Categorylookup");
            Sql(script);
        }
    }
}
