namespace Layers.Data.DataAccess.Migrations.Read
{
    using Customize;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ReadConfiguration : DbMigrationsConfiguration<Data.DataAccess.Context.ReadContext>
    {
        public ReadConfiguration()
        {
            AutomaticMigrationsEnabled = false;

            SetHistoryContextFactory("System.Data.SqlClient", (connection, defaultSchema) => new ReadHistoryContext(connection, defaultSchema));
        }

        protected override void Seed(Data.DataAccess.Context.ReadContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
