namespace Layers.Data.DataAccess.Migrations.Read
{
    using Layers.Data.DataAccess.Migrations.Shared;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteruserviewmostafa : DbMigration
    {
        public override void Up()
        {
            string script = ScriptReader.Read(MigrationContext.Read, "VW_USER_ALTERMostafa");
            Sql(script);
        }
        
        public override void Down()
        {
            string script = ScriptReader.Read(MigrationContext.Read, "VW_User_Drop");
            Sql(script);

        }
    }
}
