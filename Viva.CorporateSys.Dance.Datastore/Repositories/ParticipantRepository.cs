using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Viva.CorporateSys.Dance.Datastore.Contexts;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.Dance.Datastore.Repositories
{
    public class ParticipantRepository : OneDanceBaseRepository, IParticipantRepository
    {
        public Judge GetJudge(string emailAddress)
        {
            return DbContext.Judges.Where(x => x.Email == emailAddress).Include(x => x.JudgeCompetitions).FirstOrDefault();
        }

        public Judge GetJudge(Guid judgeId)
        {
            return DbContext.Judges.Where(x => x.Id == judgeId).Include(x => x.JudgeCompetitions).FirstOrDefault();
        }

        public Judge AddJudge(Judge Judge)
        {
            if (!DbContext.Judges.Any(x => x.Email == Judge.Email))
            {
                DbContext.Judges.Add(Judge);
                DbContext.SaveChanges();
            }
            return Judge;
        }

        public Competitor AddCompetitor(Competitor competitor)
        {
            if (!DbContext.Competitors.Any(x => x.Email == competitor.Email))
            {
                DbContext.Competitors.Add(competitor);
                DbContext.SaveChanges();
            }
            return competitor;
        }

        public ParticipantRepository()
        {
        }

        public ParticipantRepository(IOneDanceMainContext context)
        {
            base.DbContext = context;
        }


        public Competitor GetCompetitor(string emailAddress)
        {
            throw new System.NotImplementedException();
        }

        public Organisation AddOrganisation(Organisation organisation)
        {
            if (!DbContext.Organisations.Any(x => x.Caption == organisation.Caption))
            {
                DbContext.Organisations.Add(organisation);
                DbContext.SaveChanges();
            }
            return organisation;
        }
    }
}