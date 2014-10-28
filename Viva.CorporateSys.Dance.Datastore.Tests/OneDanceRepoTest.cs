using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Viva.CorporateSys.Dance.Datastore.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.Dance.Datastore.Tests
{
    [TestClass]
    public class OneDanceRepoTest
    {
        [TestInitialize]
        public void Setup()
        {
            
        }

        [TestMethod]
        public void GetAllCritera()
        {
            using (var partRepo = new ParticipantRepository())
            using (var compRepo = new CompetitionRepository())
            {
                var criteria = compRepo.GetAllCriteria();

                var comps = compRepo.GetOpenCompetitions().OrderBy(x => x.StartedOn);

                compRepo.ClearAllJudgings();

                //comps.First().JudgeCompetitions.Zip(comps.First().CompetitorCompetitions,(x1,x2)=>new {jc = x1,cc = x2}).ToList().ForEach(z =>

                var listOfJudgings = new List<Judging>();

                comps.First().JudgeCompetitions.ToList().ForEach(z=>
                {

                    var allowedCriteria = compRepo.GetAllowedCriteriaForJudge(comps.First().Id, z.Judge.Id);

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
                            ScorePoints = (criterion.Caption.StartsWith("Penalty")) ? -10 : new Random().Next(0,10)
                        };

                        listOfJudgings.Add(judging);

                        var isJudgingComplete = compRepo.IsJudgingCompleteForCompetitor(comps.First().Id,
                            cc.CompetitorId, z.Judge.Id);

                        //if (!isJudgingComplete)
                        compRepo.SubmitJudging(judging);

                    }));

                   
                });

                var listOfJudgingsGroupedByCompetitorQuery = 
                listOfJudgings.GroupBy(x => x.CompetitorCompetition.Competitor).Select(g => new
                {
                        Key= g.Key,
                        Value= g.Select(y => y).ToList()
                });

                listOfJudgingsGroupedByCompetitorQuery.ToList().ForEach(
                    c =>
                    {
                        if (!c.Value.Any(x => x.IsExcluded))
                        {
                            var orderedJudgings = c.Value.Where(x=>x.ScorePoints>0).OrderBy(x => x.ScorePoints).ToList();

                            orderedJudgings.Last().IsExcluded = true;
                            orderedJudgings.First().IsExcluded = true;

                            compRepo.SubmitJudging(orderedJudgings.Last());
                            compRepo.SubmitJudging(orderedJudgings.First());
                        }


                    });

                var listOfJudgingsGroupedByCriterionQuery = 
                listOfJudgings.GroupBy(x => new{ x.Criterion,x.CompetitorCompetition.Competitor}).Select(g => new
                {
                        Key= g.Key,
                        Value= g.Select(y => y).ToList()
                });


                var results = listOfJudgingsGroupedByCriterionQuery.OrderBy(x => x.Key.Competitor.EntityNumber)
                    .OrderBy(x => x.Key.Criterion.DisplaySequence).Select(
                        c => new
                        {
                            TeamName = c.Key.Competitor.EntityNumber + " " + c.Key.Competitor.EntityName + " " +
                                       c.Key.Competitor.Organisation.Caption,
                            CriterionSequence = c.Key.Criterion.DisplaySequence,
                            CriterionName = c.Key.Criterion.Caption,
                            CriterionAverageScore = (c.Value.Any(x => x.IsExcluded == false)) ? c.Value.Where(x => x.IsExcluded == false).Average(x => x.ScorePoints) :0
                        }
                    );

                results.OrderBy(x => x.TeamName).OrderBy(x => x.CriterionSequence).ToList().ForEach(x => Debug.WriteLine(x.TeamName + " " + x.CriterionName + " " + x.CriterionAverageScore));
            }
        }

        [TestMethod]
        public void GetCompetitions()
        {
            using(var competitionRepo = new CompetitionRepository())
            using (var participantRepo = new ParticipantRepository())
            {

                var judge    = participantRepo.GetJudge("davidy@Viva.co.nz");

                Assert.IsNotNull(judge);

                //var openCompetition = CompetitionRepo.GetLatestOpenCompetition(Participant.Id, true, true);

                //Assert.IsNotNull(openCompetition);
            }

        }
    }
}
