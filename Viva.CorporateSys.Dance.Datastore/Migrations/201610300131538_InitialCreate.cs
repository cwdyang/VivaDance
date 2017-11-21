namespace Viva.CorporateSys.Dance.Datastore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                        CategoryId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        StartedOn = c.DateTimeOffset(precision: 7),
                        CompletedOn = c.DateTimeOffset(precision: 7),
                        Location = c.String(maxLength: 255),
                        Name = c.String(maxLength: 255),
                        GroupComp = c.Byte(),
                        CompetitionStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.Category", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "Dance.CompetitorCompetition",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompetitorId = c.Guid(nullable: false),
                        CompetitionId = c.Guid(nullable: false),
                        CompetitorType = c.Int(nullable: false),
                        Sequence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.Competition", t => t.CompetitionId, cascadeDelete: true)
                .ForeignKey("Dance.Competitor", t => t.CompetitorId)
                .Index(t => t.CompetitionId)
                .Index(t => t.CompetitorId);
            
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
                "Dance.JudgeCompetition",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        JudgeId = c.Guid(nullable: false),
                        CompetitionId = c.Guid(nullable: false),
                        JudgeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.Competition", t => t.CompetitionId, cascadeDelete: true)
                .ForeignKey("Dance.Judge", t => t.JudgeId)
                .Index(t => t.CompetitionId)
                .Index(t => t.JudgeId);
            
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
                        ScorePoints = c.Double(),
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
                        JudgeCompetitionId = c.Guid(nullable: false),
                        CompetitorCompetitionId = c.Guid(nullable: false),
                        IsExcluded = c.Boolean(nullable: false),
                        ScorePoints = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Dance.BaseObject", t => t.Id)
                .ForeignKey("Dance.Criterion", t => t.CriterionId)
                .ForeignKey("Dance.JudgeCompetition", t => t.JudgeCompetitionId)
                .ForeignKey("Dance.CompetitorCompetition", t => t.CompetitorCompetitionId)
                .Index(t => t.Id)
                .Index(t => t.CriterionId)
                .Index(t => t.JudgeCompetitionId)
                .Index(t => t.CompetitorCompetitionId);
            
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
            DropForeignKey("Dance.Organisation", "Id", "Dance.BaseObject");
            DropForeignKey("Dance.Judging", "CompetitorCompetitionId", "Dance.CompetitorCompetition");
            DropForeignKey("Dance.Judging", "JudgeCompetitionId", "Dance.JudgeCompetition");
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
            DropForeignKey("Dance.CompetitorCompetition", "CompetitorId", "Dance.Competitor");
            DropForeignKey("Dance.JudgeCompetition", "JudgeId", "Dance.Judge");
            DropForeignKey("Dance.JudgeCompetition", "CompetitionId", "Dance.Competition");
            DropForeignKey("Dance.Participant", "OrganisationId", "Dance.Organisation");
            DropForeignKey("Dance.CompetitorCompetition", "CompetitionId", "Dance.Competition");
            DropForeignKey("Dance.Competition", "CategoryId", "Dance.Category");
            DropForeignKey("Dance.BaseObject", "ParentId", "Dance.BaseObject");
            DropIndex("Dance.Organisation", new[] { "Id" });
            DropIndex("Dance.Judging", new[] { "CompetitorCompetitionId" });
            DropIndex("Dance.Judging", new[] { "JudgeCompetitionId" });
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
            DropIndex("Dance.CompetitorCompetition", new[] { "CompetitorId" });
            DropIndex("Dance.JudgeCompetition", new[] { "JudgeId" });
            DropIndex("Dance.JudgeCompetition", new[] { "CompetitionId" });
            DropIndex("Dance.Participant", new[] { "OrganisationId" });
            DropIndex("Dance.CompetitorCompetition", new[] { "CompetitionId" });
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
            DropTable("Dance.JudgeCompetition");
            DropTable("Dance.Participant");
            DropTable("Dance.CompetitorCompetition");
            DropTable("Dance.Competition");
            DropTable("Dance.BaseObject");
        }
    }
}
