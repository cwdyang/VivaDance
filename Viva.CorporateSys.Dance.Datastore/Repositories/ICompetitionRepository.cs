using System;
using System.Collections.Generic;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.Dance.Datastore.Repositories
{
    public interface ICompetitionRepository:IOneDanceBaseRepository
    {
        Competition GetLatestOpenCompetitionForJudge(Guid judgeId);
        IList<Competition> GetOpenCompetitionsForJudge(Guid judgeId);
        bool IsJudgingCompleteForCompetition(Guid competitionId, Guid? judgeId);
        bool IsJudgingCompleteForCompetitor(Guid competitionId, Guid competitorId, Guid? judgeId);
        IList<Criterion> GetAllowedCriteriaForJudge(Guid competitionId,Guid judgeId);
        IList<Competitor> GetCompetitorsForCompetition(Guid competitionId);
        IList<Judge> GetJudgesForCompetition(Guid competitionId);
        IList<Criterion> GetAllCriteria();
        IList<Judging> GetJudgingsForCompetitor(Guid competitionId,Guid competitorId);
        Judging SubmitJudging(Judging judging);
        IList<Competition> GetOpenCompetitions();
        IList<Competition> GetNotClosedCompetitions();
        void ClearAllJudgings();
    }
}