namespace Layers.Data.DataAccess.Migrations.Write
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenerateFirstPhaseOfDataBaseTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerText = c.String(),
                        QuestionId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                        QuizId = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        Type = c.Int(nullable: false),
                        ChannelId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Channel", t => t.ChannelId, cascadeDelete: true)
                .Index(t => t.ChannelId);
            
            CreateTable(
                "dbo.Channel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Logo = c.String(),
                        Description = c.String(),
                        Bio = c.String(),
                        BankAccountNumber = c.String(),
                        BankId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LK_Bank", t => t.BankId, cascadeDelete: true)
                .Index(t => t.BankId);
            
            CreateTable(
                "dbo.LK_Bank",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniqueKey = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LK_Bank_Local",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LK_Language", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.LK_Bank", t => t.BankId, cascadeDelete: true)
                .Index(t => t.BankId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.ContentGoals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Info = c.String(),
                        ContentId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
            CreateTable(
                "dbo.ContentRequirements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Info = c.String(),
                        ContentId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
            CreateTable(
                "dbo.ContentTargetViewers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Info = c.String(),
                        ContentId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
            CreateTable(
                "dbo.LiveStreamSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        ContentId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
            CreateTable(
                "dbo.RecordedVideos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        URL = c.String(),
                        ContentId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
            CreateTable(
                "dbo.FAQs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        RecordedVideoId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecordedVideos", t => t.RecordedVideoId, cascadeDelete: true)
                .Index(t => t.RecordedVideoId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ContentId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
            CreateTable(
                "dbo.Leasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SectionId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.WebinarSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        ContentId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .Index(t => t.ContentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.WebinarSessions", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.Leasons", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Sections", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.FAQs", "RecordedVideoId", "dbo.RecordedVideos");
            DropForeignKey("dbo.RecordedVideos", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.Quizs", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.LiveStreamSessions", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.ContentTargetViewers", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.ContentRequirements", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.ContentGoals", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.Contents", "ChannelId", "dbo.Channel");
            DropForeignKey("dbo.Channel", "BankId", "dbo.LK_Bank");
            DropForeignKey("dbo.LK_Bank_Local", "BankId", "dbo.LK_Bank");
            DropForeignKey("dbo.LK_Bank_Local", "LanguageId", "dbo.LK_Language");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.WebinarSessions", new[] { "ContentId" });
            DropIndex("dbo.Leasons", new[] { "SectionId" });
            DropIndex("dbo.Sections", new[] { "ContentId" });
            DropIndex("dbo.FAQs", new[] { "RecordedVideoId" });
            DropIndex("dbo.RecordedVideos", new[] { "ContentId" });
            DropIndex("dbo.LiveStreamSessions", new[] { "ContentId" });
            DropIndex("dbo.ContentTargetViewers", new[] { "ContentId" });
            DropIndex("dbo.ContentRequirements", new[] { "ContentId" });
            DropIndex("dbo.ContentGoals", new[] { "ContentId" });
            DropIndex("dbo.LK_Bank_Local", new[] { "LanguageId" });
            DropIndex("dbo.LK_Bank_Local", new[] { "BankId" });
            DropIndex("dbo.Channel", new[] { "BankId" });
            DropIndex("dbo.Contents", new[] { "ChannelId" });
            DropIndex("dbo.Quizs", new[] { "ContentId" });
            DropIndex("dbo.Questions", new[] { "QuizId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.WebinarSessions");
            DropTable("dbo.Leasons");
            DropTable("dbo.Sections");
            DropTable("dbo.FAQs");
            DropTable("dbo.RecordedVideos");
            DropTable("dbo.LiveStreamSessions");
            DropTable("dbo.ContentTargetViewers");
            DropTable("dbo.ContentRequirements");
            DropTable("dbo.ContentGoals");
            DropTable("dbo.LK_Bank_Local");
            DropTable("dbo.LK_Bank");
            DropTable("dbo.Channel");
            DropTable("dbo.Contents");
            DropTable("dbo.Quizs");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
