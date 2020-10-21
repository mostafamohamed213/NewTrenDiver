using Layers.Data.DataAccess.Migrations.Write;
using Layers.Base.Entities.Write;
using Layers.Base.Entities.Write.Lookups;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.DataAccess.Context
{
    public class WriteContext : DbContext
    {
        public WriteContext() : base("name=WriteEntities")
        {
             Database.SetInitializer(new MigrateDatabaseToLatestVersion<WriteContext, WriteConfiguration>("WriteEntities"));
            //Database.SetInitializer<WriteContext>(null);
        }

        public DbSet<LanguageLookupLoclize> LanguageLookupLoclize { get; set; }
        public DbSet<LanguageLookup> LanguageLookup { get; set; }
        public DbSet<BankLookup> BankLookup { get; set; }
        public DbSet<BankLookupLoclize> BankLookupLoclize { get; set; }
        public DbSet<CategoryLookup> CategoryLookup { get; set; }
        public DbSet<CategoryLookupLocalize> categoryLookupLocalize { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<ContentGoal> ContentGoal { get; set; }
        public DbSet<ContentRequirement> ContentRequirement { get; set; }
        public DbSet<ContentTargetViewer> ContentTargetViewer { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<Leason> Leason { get; set; }
        public DbSet<LiveStreamSession> LiveStreamSession { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Quiz> Quizze { get; set; }
        public DbSet<RecordedVideo> RecordedVideo { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<WebinarSession> WebinarSession { get; set; }

        public DbSet<PurchasedContents> PurchasedContents { get; set; }













    }
}
