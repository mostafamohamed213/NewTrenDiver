using Layers.Base.Entities.Read;
using Layers.Data.DataAccess.Migrations.Read;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.DataAccess.Context
{
    public class ReadContext : DbContext
    {
        public ReadContext() : base("name=ReadEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ReadContext, ReadConfiguration>("ReadEntities"));

        }

        public DbSet<User> User { get; set; }
        public DbSet<Channel> Channel { get; set; }

        public DbSet<Content> Content { get; set; }

        public DbSet<ContentGoal> ContentGoals { get; set; }

        public DbSet<ContentRequirement> ContentRequirements { get; set; }

        public DbSet<ContentTargetViewer> ContentTargetViewers  { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Lessons> Lessons { get; set; }

        public DbSet<PurchasedContents> purchasedContents { get; set; }
        public DbSet<CategoryLookup> CategoryLookup { get; set; }
        public DbSet<CategoryLookupLocalize> categoryLookupLocalize { get; set; }

        public DbSet<LanguageLookupView> LanguageLookup { get; set; }
        public DbSet<LanguageLookupLoclize> LanguageLookupLoclize { get; set; }
       
    }

}
