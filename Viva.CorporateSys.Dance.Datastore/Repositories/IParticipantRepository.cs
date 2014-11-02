using System;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.Dance.Datastore.Repositories
{
    public interface IParticipantRepository : IOneDanceBaseRepository
    {
        Judge GetJudge(Guid judgeId);
        Judge GetJudge(string emailAddress);
        Competitor GetCompetitor(string emailAddress);
        Organisation AddOrganisation(Organisation organisation);
        Competitor AddCompetitor(Competitor competitor);

    }
}