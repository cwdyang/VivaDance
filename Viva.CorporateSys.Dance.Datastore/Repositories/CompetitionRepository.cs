using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva.CorporateSys.Dance.Datastore.Contexts;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.Dance.Datastore.Repositories
{
    public class CompetitionRepository :OneDanceBaseRepository, ICompetitionRepository
    {
        public static int MinJudgingPerCriterion = 3;
        public static int NormalCriterionCount = 5;
        public static int PenalityCriterionCount = 1;

        public CompetitionRepository()
        {
        }

        public CompetitionRepository(IOneDanceMainContext context)
        {
            base.DbContext = context;
        }

        public IList<Competition> GetOpenCompetitionsForJudge(Guid judgeId)
        {
            var list = GetOpenCompetitions().Where(x => x.JudgeCompetitions.First().JudgeId == judgeId);
            return list.ToList();
        }

        public IList<Competition> GetOpenCompetitions()
        {
            var list = DbContext.JudgeCompetitions.Select(x => x.Competition)
                .Include(x => x.JudgeCompetitions.Select(y=>y.Judge).Select(z=>z.Organisation))
                .Include(x => x.CompetitorCompetitions.Select(y => y.Competitor).Select(z => z.Organisation))
                .Where(
                    x =>
                        !new List<CompetitionStatus> {CompetitionStatus.Closed, CompetitionStatus.JudgingCompleted}
                            .Contains(x.CompetitionStatus))
                ;
            return list.ToList();
        }

        public bool IsJudgingCompleteForCompetition(Guid competitionId, Guid? judgeId)
        {
            return false;
        }

        public bool IsJudgingCompleteForCompetitor(Guid competitionId, Guid competitorId, Guid? judgeId)
        {
           var queryJudgings= DbContext.Judgings.Where(x =>
                x.CompetitorCompetition.CompetitionId == competitionId &&
                x.CompetitorCompetition.CompetitorId == competitorId &&
                (x.JudgeCompetition.JudgeId == judgeId || judgeId==null));

            var queryJudgingsList = queryJudgings.ToList();

            var judgingCount = queryJudgings.Select(x => x.Id).Distinct().Count();
            var judgingGroupCount = queryJudgings.GroupBy(x => x.JudgeCompetition.JudgeId).Select(gr => new { Key = gr.Key, Count = gr.Count() });

            var judgingGroupCounts = judgingGroupCount.ToList();

            var judge = DbContext.Judges.FirstOrDefault(x => x.Id == judgeId);



            var judgingCountCap = (judge == null)
                ? (MinJudgingPerCriterion*NormalCriterionCount + PenalityCriterionCount)
                : MinJudgingPerCriterion + ((judge.JudgeCompetitions.First().JudgeType == JudgeType.Head)
                    ? PenalityCriterionCount
                    : 0);


            var isComplete = (judgingCount >= judgingCountCap) &&
               judgingGroupCount.All(x => x.Count >= judgingCountCap);

            return isComplete;
        }


        public IList<Criterion> GetAllowedCriteriaForJudge(Guid competitionId, Guid judgeId)
        {
            var judgingComp = DbContext.JudgeCompetitions.FirstOrDefault(x => x.CompetitionId == competitionId && x.JudgeId == judgeId);
            var listAssigned = DbContext.JudgingAssignments.Where(y => y.JudgeId == judgeId).Select(x => x.Criterion).Include(x=>x.JudgingAssignments);
            var penaltyCriterion = DbContext.Criteria.FirstOrDefault(x => x.Caption == "Penalty");
            List<Criterion> listAssignedToReturn=listAssigned.ToList();

            if (judgingComp == null)
                return Enumerable.Empty<Criterion>().ToList();

            if (judgingComp.JudgeType == JudgeType.Head && !listAssignedToReturn.Contains(penaltyCriterion))
            {
                listAssignedToReturn.Add(penaltyCriterion);
            }

            if (judgingComp.JudgeType == JudgeType.Normal && listAssignedToReturn.Contains(penaltyCriterion))
            {
                listAssignedToReturn.Remove(penaltyCriterion);
            }


            return listAssignedToReturn;
        }

        public IList<Competitor> GetCompetitorsForCompetition(Guid competitionId)
        {
            var list = DbContext.CompetitorCompetitions.Where(x => x.CompetitionId == competitionId).Select(x=>x.Competitor).Include(x=>x.CompetitorCompetitions);

            return list.ToList();
        }

        public IList<Judge> GetJudgesForCompetition(Guid competitionId)
        {
            var list = DbContext.JudgeCompetitions.Where(x => x.CompetitionId == competitionId).Select(x => x.Judge).Include(x=>x.JudgeCompetitions);
            return list.ToList();
        }

        public IList<Criterion> GetAllCriteria()
        {
            return DbContext.Criteria.ToList();
        }

        public IList<Judging> GetJudgingsForCompetitor(Guid competitionId, Guid competitorId)
        {
            var list =
                DbContext.Judgings.Where(
                    x =>
                        x.CompetitorCompetition.CompetitionId == competitionId &&
                        x.CompetitorCompetition.CompetitorId == competitorId)
                        .Include(x => x.JudgeCompetition)
                        .Include(x=>x.CompetitorCompetition);
            return list.ToList();
        }

        public Judging SubmitJudging(Judging judging)
        {
            DbContext.JudgeCompetitions.Attach(judging.JudgeCompetition);
            DbContext.CompetitorCompetitions.Attach(judging.CompetitorCompetition);


            var judgingFound = DbContext.Judgings.FirstOrDefault(x=>
                x.CriterionId == judging.Criterion.Id &&
                x.CompetitorCompetitionId==judging.CompetitorCompetition.Id&&
                x.JudgeCompetitionId==judging.JudgeCompetition.Id);

            if (judgingFound != null)
            {
                judgingFound.IsExcluded = judging.IsExcluded;
                DbContext.Judgings.AddOrUpdate(judgingFound);
            }
            else
            {
                DbContext.Judgings.Add(judging);
                judgingFound = judging;
            }

            DbContext.SaveChanges();
            return judgingFound;
        }

        public void ClearAllJudgings()
        {
            ((DbContext)DbContext).DeleteAll<Judging>();
            DbContext.SaveChanges();
        }
    }
}
