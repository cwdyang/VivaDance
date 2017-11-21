using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Viva.CorporateSys.Dance.Datastore.Mappings;
//using Viva.CorporateSys.Dance.Datastore.Migrations;
using Viva.CorporateSys.Dance.Datastore.Migrations;
using Viva.CorporateSys.Dance.Domain.Models;

using System.Data.Linq;

namespace Viva.CorporateSys.Dance.Datastore.Contexts
{
    /// <summary>
    /// To enable database creation:
    /// 
    /// in Package managment console:
    /// 
    /// run
    /// 
    /// Enable-Migrations
    /// Add-Migration "name of the migration, can be anything" > ends up @
    /// c:\development\oneDance\Viva.CorporateSys.Dance.Datastore\Migrations\201410062249468_Dance.cs
    /// 
    /// Add-Migration InitialMigrations 
    /// 
    /// generates a blank up/down() and 
    /// 
    /// Update-Database runs new changess and runs seed method
    /// 
    /// To take down the database:
    /// 
    /// ALTER DATABASE OneDance SET OFFLINE WITH
    /// ROLLBACK IMMEDIATE
    /// drop database OneDance
    /// go
    /// 
    /// delete mdfs and ldfs from C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\
    /// 
    /// we are using
    /// Inheritance with EF Code First: Part 3 – Table per Concrete Type (TPC)
    /// 
    /// </summary>
    public class OneDanceMainContext : DbContext, IOneDanceMainContext
    {

        public string ConnectionString
        {
            get { return Database.Connection.ConnectionString; }
            private set { Database.Connection.ConnectionString = value; }
        }

        public IDbSet<BaseObject> BaseObjects { get; set; }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Competition> Competitions { get; set; }
        public IDbSet<Competitor> Competitors { get; set; }
        public IDbSet<Criterion> Criteria { get; set; }

        public IDbSet<Division> Divisions { get; set; }

        public IDbSet<Judge> Judges { get; set; }
        public IDbSet<Judging> Judgings { get; set; }
        public IDbSet<JudgingAssignment> JudgingAssignments { get; set; }
        public IDbSet<JudgeCompetition> JudgeCompetitions { get; set; }
        public IDbSet<CompetitorCompetition> CompetitorCompetitions { get; set; }

        public IDbSet<Organisation> Organisations { get; set; }
        public IDbSet<Participant> Participants { get; set; }

        //YOU NEED TO INSTALL EF6 nuget in the host application
        public OneDanceMainContext()
            : base("name=dbOneDance")
		{
			Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
                
            Database.SetInitializer<OneDanceMainContext>(null);

            /*
             * performance 
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
            */

            if (!Database.Exists())
            {
                ((IObjectContextAdapter)this).ObjectContext.CreateDatabase();
                new Configuration().SeedDb(this);
            }

           

		}

        public void Reseed()
        {
            new Configuration().SeedDb(this);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Note the order of configuration matters
            //http://entityframework.codeplex.com/workitem/1646
            modelBuilder.Configurations.Add(new BaseObjectMapping());

            modelBuilder.Configurations.Add(new DivisionMapping());

            modelBuilder.Configurations.Add(new JudgingMapping());
            modelBuilder.Configurations.Add(new JudgeMapping());
            modelBuilder.Configurations.Add(new JudgingAssignmentMapping());

            modelBuilder.Configurations.Add(new JudgeCompetitionMapping());
            modelBuilder.Configurations.Add(new CompetitorCompetitionMapping());
            
            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new CriterionMapping());
            modelBuilder.Configurations.Add(new CompetitionMapping());
            modelBuilder.Configurations.Add(new CompetitorMapping());

            modelBuilder.Configurations.Add(new ParticipantMapping());
            modelBuilder.Configurations.Add(new OrganisationMapping());
            
            
        }



        public static string SchemaName = "Dance";



        
    }

    public static class DbContextExtensions
    {
        public static void DeleteAll<T>(this DbContext context)
            where T : class
        {
            foreach (var p in context.Set<T>())
            {
                context.Entry(p).State = EntityState.Deleted;
            }
        }
    }
}
