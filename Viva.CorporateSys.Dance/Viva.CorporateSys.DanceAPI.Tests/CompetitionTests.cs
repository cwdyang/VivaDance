using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Viva.CorporateSys.DanceAPI.Tests
{
    [TestClass]
    public class CompetitionTests
    {
        [TestMethod]
        public void CompetitionTest()
        {
            var compSvc = new CompetitionService();
            var criteria = compSvc.GetAllCriteria();

            var comps = compSvc.GetOpenCompetitions().OrderBy(x => x.StartedOn);

            compSvc.ClearAllJudgings();

            //comps.First().JudgeCompetitions.Zip(comps.First().CompetitorCompetitions,(x1,x2)=>new {jc = x1,cc = x2}).ToList().ForEach(z =>

            var listOfJudgings = new List<Judging>();

            comps.First().JudgeCompetitions.ToList().ForEach(z =>
            {

                var allowedCriteria = compSvc.GetAllowedCriteriaForJudge(comps.First().Id, z.Judge.Id);

                var competitors = comps.First()
                    .CompetitorCompetitions.Where(c => c.CompetitionId == z.CompetitionId);



                competitors.ToList().ForEach(cc => allowedCriteria.ToList().ForEach(criterion =>
                {
                    var judging = new Judging
                    {
                        CompetitorCompetition = cc,
                        JudgeCompetition = z,
                        Id = Guid.NewGuid(),
                        Criterion = criterion,
                        ScorePoints = (criterion.Text.StartsWith("Penalty")) ? -10 : new Random().Next(0, 10)
                    };

                    listOfJudgings.Add(judging);

                    var isJudgingComplete = compSvc.IsJudgingCompleteForCompetitor(comps.First().Id,
                        cc.Competitor.Id, z.Judge.Id);

                    //if (!isJudgingComplete)
                    compSvc.SubmitJudging(judging);

                }));


            });

            var listOfJudgingsGroupedByCompetitorQuery =
                listOfJudgings.GroupBy(x => x.CompetitorCompetition.Competitor).Select(g => new
                {
                    Key = g.Key,
                    Value = g.Select(y => y).ToList()
                });

            listOfJudgingsGroupedByCompetitorQuery.ToList().ForEach(
                c =>
                {
                    if (!c.Value.Any(x => x.IsExcluded))
                    {
                        var orderedJudgings = c.Value.Where(x => x.ScorePoints > 0).OrderBy(x => x.ScorePoints).ToList();

                        orderedJudgings.Last().IsExcluded = true;
                        orderedJudgings.First().IsExcluded = true;

                        compSvc.SubmitJudging(orderedJudgings.Last());
                        compSvc.SubmitJudging(orderedJudgings.First());
                    }
                });



            listOfJudgingsGroupedByCompetitorQuery.ToList().ForEach(

                c =>
                {
                    Debug.WriteLine(c.Key.EntityName);

                    var total = 0.0;

                    var criteriaIds = c.Value.Select(x => x.Criterion.Id).Distinct().ToList();


                    criteriaIds.ForEach(cr =>
                        {
                            var criterion = c.Value.First(x => x.Criterion.Id == cr).Criterion;
                            var scoreQuery = c.Value.Where(x => x.Criterion.Id == cr && !x.IsExcluded);
                            var scores = scoreQuery.ToList();
                            var scoreAvg = scoreQuery.Average(x => x.ScorePoints);

                            Debug.WriteLine(criterion.Text + "__" + scoreQuery.Count() + "__" + scoreAvg);

                            total += scoreAvg;
                        });

                    Debug.WriteLine("Total:" + total);
                });


            /*
            var results = listOfJudgingsGroupedByCriterionQuery.OrderBy(x => x.Key.Competitor.EntityNumber)
                .OrderBy(x => x.Key.Criterion.DisplaySequence).Select(
                    c => new
                    {
                        TeamName = c.Key.Competitor.EntityNumber + " " + c.Key.Competitor.EntityName + " " +
                                   c.Key.Competitor.OrganisationName,
                        CriterionSequence = c.Key.Criterion.DisplaySequence,
                        CriterionName = c.Key.Criterion.Text,
                        CriterionAverageScore =
                            (c.Value.Any(x => x.IsExcluded == false))
                                ? c.Value.Where(x => x.IsExcluded == false).Average(x => x.ScorePoints)
                                : 0
                    }
                );

            results.OrderBy(x => x.TeamName)
                .OrderBy(x => x.CriterionSequence)
                .ToList()
                .ForEach(x => Debug.WriteLine(x.TeamName + " " + x.CriterionName + " " + x.CriterionAverageScore));
             */
        }
    }
}


