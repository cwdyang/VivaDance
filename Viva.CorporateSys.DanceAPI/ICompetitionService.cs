using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Viva.CorporateSys.DanceAPI
{
    [ServiceContract]
    public interface ICompetitionService
    {
       
        [OperationContract]
        List<Competition> GetOpenCompetitionsForJudge(Guid judgeId);
        [OperationContract]
        List<Competition> GetOpenCompetitions();
        [OperationContract]
        bool IsJudgingCompleteForCompetition(Guid competitionId, Guid? judgeId);
        [OperationContract]
        bool IsJudgingCompleteForCompetitor(Guid competitionId, Guid competitorId, Guid? judgeId);
        [OperationContract]
        List<Criterion> GetAllowedCriteriaForJudge(Guid competitionId, Guid judgeId);
        [OperationContract]
        List<Competitor> GetCompetitorsForCompetition(Guid competitionId);
        [OperationContract]
        List<Judge> GetJudgesForCompetition(Guid competitionId);
        [OperationContract]
        List<Criterion> GetAllCriteria();
        [OperationContract]
        List<Judging> GetJudgingsForCompetitor(Guid competitionId, Guid competitorId);
        [OperationContract]
        Judging SubmitJudging(Judging judging);
        [OperationContract]
        void ClearAllJudgings();


    }

    
}