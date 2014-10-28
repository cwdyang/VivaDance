namespace Viva.CorporateSys.Dance.Datastore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            return;
            CreateTable(
                "Dance.BaseObject",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(maxLength: 50),
                        IsEnabled = c.Boolean(nullable: false),
                        DisplaySequence = c.Int(),
                        Caption = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.BaseObject", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "Dance.Competition",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompetitorId = c.Guid(nullable: false),
                        JudgeId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        StartedOn = c.DateTimeOffset(precision: 7),
                        CompletedOn = c.DateTimeOffset(precision: 7),
                        Location = c.String(maxLength: 255),
                        CompetitionType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.Category", t => t.CategoryId)
                .ForeignKey("Dance.Competitor", t => t.CompetitorId)
                .ForeignKey("Dance.Judge", t => t.JudgeId)
                .Index(t => t.CategoryId)
                .Index(t => t.CompetitorId)
                .Index(t => t.JudgeId);
            
            CreateTable(
                "Dance.Participant",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EntityName = c.String(maxLength: 255),
                        EntityNumber = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        MobileNumber = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255),
                        OrganisationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.Organisation", t => t.OrganisationId)
                .Index(t => t.OrganisationId);
            
            CreateTable(
                "Dance.Category",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DivisionId = c.Guid(nullable: false),
                        Requirements = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.BaseObject", t => t.Id)
                .ForeignKey("Dance.Division", t => t.DivisionId)
                .Index(t => t.Id)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "Dance.Competitor",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompetitorType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.Participant", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Dance.Criterion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.BaseObject", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Dance.Division",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.BaseObject", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Dance.Judge",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        JudgeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.Participant", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Dance.JudgingAssignment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        JudgeId = c.Guid(nullable: false),
                        CriterionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.BaseObject", t => t.Id)
                .ForeignKey("Dance.Judge", t => t.JudgeId)
                .ForeignKey("Dance.Criterion", t => t.CriterionId)
                .Index(t => t.Id)
                .Index(t => t.JudgeId)
                .Index(t => t.CriterionId);
            
            CreateTable(
                "Dance.Judging",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CriterionId = c.Guid(nullable: false),
                        JudgeId = c.Guid(nullable: false),
                        CompetitorId = c.Guid(nullable: false),
                        CompetitionId = c.Guid(nullable: false),
                        IsExcluded = c.Boolean(nullable: false),
                        ScorePoint = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.BaseObject", t => t.Id)
                .ForeignKey("Dance.Criterion", t => t.CriterionId)
                .ForeignKey("Dance.Judge", t => t.JudgeId)
                .ForeignKey("Dance.Competitor", t => t.CompetitorId)
                .ForeignKey("Dance.Competition", t => t.CompetitionId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.CriterionId)
                .Index(t => t.JudgeId)
                .Index(t => t.CompetitorId)
                .Index(t => t.CompetitionId);
            
            CreateTable(
                "Dance.Organisation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.BaseObject", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            return;
            DropForeignKey("Dance.Organisation", "Id", "Dance.BaseObject");
            DropForeignKey("Dance.Judging", "CompetitionId", "Dance.Competition");
            DropForeignKey("Dance.Judging", "CompetitorId", "Dance.Competitor");
            DropForeignKey("Dance.Judging", "JudgeId", "Dance.Judge");
            DropForeignKey("Dance.Judging", "CriterionId", "Dance.Criterion");
            DropForeignKey("Dance.Judging", "Id", "Dance.BaseObject");
            DropForeignKey("Dance.JudgingAssignment", "CriterionId", "Dance.Criterion");
            DropForeignKey("Dance.JudgingAssignment", "JudgeId", "Dance.Judge");
            DropForeignKey("Dance.JudgingAssignment", "Id", "Dance.BaseObject");
            DropForeignKey("Dance.Judge", "Id", "Dance.Participant");
            DropForeignKey("Dance.Division", "Id", "Dance.BaseObject");
            DropForeignKey("Dance.Criterion", "Id", "Dance.BaseObject");
            DropForeignKey("Dance.Competitor", "Id", "Dance.Participant");
            DropForeignKey("Dance.Category", "DivisionId", "Dance.Division");
            DropForeignKey("Dance.Category", "Id", "Dance.BaseObject");
            DropForeignKey("Dance.Competition", "JudgeId", "Dance.Judge");
            DropForeignKey("Dance.Competition", "CompetitorId", "Dance.Competitor");
            DropForeignKey("Dance.Participant", "OrganisationId", "Dance.Organisation");
            DropForeignKey("Dance.Competition", "CategoryId", "Dance.Category");
            DropForeignKey("Dance.BaseObject", "ParentId", "Dance.BaseObject");
            DropIndex("Dance.Organisation", new[] { "Id" });
            DropIndex("Dance.Judging", new[] { "CompetitionId" });
            DropIndex("Dance.Judging", new[] { "CompetitorId" });
            DropIndex("Dance.Judging", new[] { "JudgeId" });
            DropIndex("Dance.Judging", new[] { "CriterionId" });
            DropIndex("Dance.Judging", new[] { "Id" });
            DropIndex("Dance.JudgingAssignment", new[] { "CriterionId" });
            DropIndex("Dance.JudgingAssignment", new[] { "JudgeId" });
            DropIndex("Dance.JudgingAssignment", new[] { "Id" });
            DropIndex("Dance.Judge", new[] { "Id" });
            DropIndex("Dance.Division", new[] { "Id" });
            DropIndex("Dance.Criterion", new[] { "Id" });
            DropIndex("Dance.Competitor", new[] { "Id" });
            DropIndex("Dance.Category", new[] { "DivisionId" });
            DropIndex("Dance.Category", new[] { "Id" });
            DropIndex("Dance.Competition", new[] { "JudgeId" });
            DropIndex("Dance.Competition", new[] { "CompetitorId" });
            DropIndex("Dance.Participant", new[] { "OrganisationId" });
            DropIndex("Dance.Competition", new[] { "CategoryId" });
            DropIndex("Dance.BaseObject", new[] { "ParentId" });
            DropTable("Dance.Organisation");
            DropTable("Dance.Judging");
            DropTable("Dance.JudgingAssignment");
            DropTable("Dance.Judge");
            DropTable("Dance.Division");
            DropTable("Dance.Criterion");
            DropTable("Dance.Competitor");
            DropTable("Dance.Category");
            DropTable("Dance.Participant");
            DropTable("Dance.Competition");
            DropTable("Dance.BaseObject");
        }
    }
}
