using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.Dance.Datastore.Contexts
{
    public interface IOneDanceMainContext
    {
        string ConnectionString { get; }

        IDbSet<BaseObject> BaseObjects { get; set; }

        IDbSet<Category> Categories { get; set; }
        IDbSet<Competition> Competitions { get; set; }
        IDbSet<Competitor> Competitors { get; set; }
        IDbSet<Criterion> Criteria { get; set; }

        IDbSet<Division> Divisions { get; set; }

        IDbSet<Judge> Judges { get; set; }
        IDbSet<Judging> Judgings { get; set; }
        IDbSet<JudgingAssignment> JudgingAssignments { get; set; }
        IDbSet<JudgeCompetition> JudgeCompetitions { get; set; }
        IDbSet<CompetitorCompetition> CompetitorCompetitions { get; set; }

        IDbSet<Organisation> Organisations { get; set; }
        IDbSet<Participant> Participants { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class ;

        void Dispose();
		int SaveChanges();
    }
}
