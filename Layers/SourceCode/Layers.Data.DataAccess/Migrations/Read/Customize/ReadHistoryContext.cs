using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.DataAccess.Migrations.Read.Customize
{
    public class ReadHistoryContext : HistoryContext
    {
        public ReadHistoryContext(DbConnection dbConnection, string defaultSchema) : base(dbConnection, defaultSchema) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().ToTable(tableName: "__MigrationHistory_Read", schemaName: "dbo");
        }
    }
}
