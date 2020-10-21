namespace Layers.Data.DataAccess.Migrations.Write
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class WriteConfiguration : DbMigrationsConfiguration<Data.DataAccess.Context.WriteContext>
    {
        public WriteConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.DataAccess.Context.WriteContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
