using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            var criteria = compSvc.GetAllCriteria().OrderBy(x => x.DisplaySequence);

            var comps = compSvc.GetOpenCompetitions().OrderBy(x => x.StartedOn);

            compSvc.ClearAllJudgings();

            //comps.First().JudgeCompetitions.Zip(comps.First().CompetitorCompetitions,(x1,x2)=>new {jc = x1,cc = x2}).ToList().ForEach(z =>

            var report = new StringBuilder();

            report.Append("<style type='text/css' media='screen'>table{border-collapse:collapse;border:1px solid #FF0000;}table td{border:1px solid #FF0000;}</style><table>");

            comps.ToList().ForEach(comp =>
            {
                var listOfJudgings = new List<Judging>();



                report.Append("<tr>");
                report.Append("<td>" + "&nbsp;" + "</td><td colspan='6'>" + "" + "</td>");
                report.Append("</tr>");
                report.Append("<tr>");
                report.Append("<td>" + "&nbsp;" + "</td><td colspan='6'>" + "" + "</td>");
                report.Append("</tr>");
                report.Append("<tr>");
                report.Append("<td>" + "Competition" + "</td><td colspan='6'>" + comp.Text + "</td>");
                report.Append("</tr>");
                report.Append("<tr>");
                report.Append("<td>" + "Division" + "</td><td colspan='6'>" + comp.DivisionName + "</td>");
                report.Append("</tr>");
                report.Append("<tr>");
                report.Append("<td>" + "Category" + "</td><td colspan='6'>" + comp.CategoryName + "</td>");
                report.Append("</tr>");
                report.Append("<tr>");
                report.Append("<td>" + "Location" + "</td><td colspan='6'>" + comp.Location + "</td>");
                report.Append("</tr>");
                report.Append("<tr>");
                report.Append("<td>" + "Start" + "</td><td colspan='6'>" + comp.StartedOn.Value.ToString("yyyy/MMM/dd HH:mm:ss") + "</td>");
                report.Append("</tr>");
                

                comp.JudgeCompetitions.ToList().ForEach(z =>
                {

                    var allowedCriteria = compSvc.GetAllowedCriteriaForJudge(comps.First().Id, z.Judge.Id);

                    var competitors = comp.CompetitorCompetitions.Where(c => c.CompetitionId == z.CompetitionId);



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

                listOfJudgingsGroupedByCompetitorQuery.ToList().ForEach(c =>
                {
                    if (!c.Value.Any(x => x.IsExcluded))
                    {
                        var orderedJudgings =
                            c.Value.Where(x => x.ScorePoints > 0).OrderBy(x => x.ScorePoints).ToList();

                        orderedJudgings.Last().IsExcluded = true;
                        orderedJudgings.First().IsExcluded = true;

                        compSvc.SubmitJudging(orderedJudgings.Last());
                        compSvc.SubmitJudging(orderedJudgings.First());
                    }
                });

                var rankings = new Dictionary<Competitor, double>();

                listOfJudgingsGroupedByCompetitorQuery.ToList().ForEach(c =>
                {
                    
                    report.Append("<tr>");
                    report.Append("<td>" + "&nbsp;" + "</td><td colspan='6'>" + "" + "</td>");
                    report.Append("</tr>");
                    report.Append("<tr>");
                    report.Append("<td>" + "&nbsp;" + "</td><td colspan='6'>" + "" + "</td>");
                    report.Append("</tr>");
                    report.Append("<tr>");
                    report.Append("<td>" + "Team #" + "</td><td colspan='6'>" + c.Key.EntityNumber + "</td>");
                    report.Append("</tr>");
                    report.Append("<tr>");
                    report.Append("<td>" + "Team Name" + "</td><td colspan='6'>" + c.Key.EntityName + " " + c.Key.FirstName + " " + c.Key.LastName + "</td>");
                    report.Append("</tr>");
                    report.Append("<tr>");
                    report.Append("<td>" + "&nbsp;" + "</td><td colspan='6'>" + "" + "</td>");
                    report.Append("</tr>");

                    var total = 0.0;

                    var criteriaIds = criteria.OrderBy(x=>x.DisplaySequence).Select(x => x.Id).Distinct().ToList(); //c.Value.Select(x => x.Criterion.Id).Distinct().ToList();

                    var judges = c.Value.Select(x=>x.JudgeCompetition.Judge).Distinct().OrderBy(x=>x.LastName).ThenBy(x=>x.FirstName).ToList();

                    var judgeValues = "";

                    judges.ForEach(x => judgeValues += "<td>" + x.FirstName + "<br/>" + x.LastName + "</td>");

                    report.Append("<tr>");
                    report.Append("<td>" + "Judges" + "</td>" + judgeValues + "<td></td>");
                    report.Append("</tr>");



                    criteriaIds.ForEach(cr =>
                    {
                        var judgingMatched = c.Value.FirstOrDefault(x => x.Criterion.Id == cr);
                        var criterion = (judgingMatched != null) ? judgingMatched.Criterion : criteria.FirstOrDefault(x => x.Id == cr);
                        var scoresAllQuery = c.Value.Where(x => x.Criterion.Id == cr);
                        var scoreQuery = scoresAllQuery.Where(x => !x.IsExcluded);
                        var scores = scoreQuery.ToList();
                        var scoresAll =
                            scoresAllQuery.OrderBy(x => x.JudgeCompetition.Judge.LastName)
                                .ThenBy(x => x.JudgeCompetition.Judge.FirstName)
                                .ToList();
                        var scoreAvg = (scores.Count==0)?0:scoreQuery.Average(x => x.ScorePoints);



                        var scoreValues = "";

                        var cursor = 0;

                        judges.ForEach(x =>
                        {
                            var matchedJudging = scoresAll.FirstOrDefault(y => y.JudgeCompetition.Judge.Id == x.Id);

                            if (matchedJudging == null)
                                scoreValues += "<td></td>";
                            else
                                scoreValues += "<td>" + ((matchedJudging.IsExcluded) ? "<strike><b><font color='red'>" : "") +
                                               matchedJudging.ScorePoints +
                                               ((matchedJudging.IsExcluded) ? "</font></b></strike>" : "") + "</td>";
                        });

                       
                        report.Append("<tr>");
                        report.Append("<td>" + criterion.Text + "</td>" + scoreValues + "<td>" + scoreAvg + "</td>");
                        report.Append("</tr>");

                        total += scoreAvg;
                    });

                    rankings.Add(c.Key, total);

                    report.Append("<tr>");
                    report.Append("<td>" + "Total" + "</td><td colspan='6'>" + total + "</td>");
                    report.Append("</tr>");

                });

                report.Append("</table><br/><table><tr>");
                report.Append("<td>Rankings</td><td colspan='3'></td>");
                report.Append("</tr>");

                report.Append("<tr>");
                report.Append("<td>Placing</td><td>Score</td><td>Team #</td><td>Team Name</td>");
                report.Append("</tr>");

                var placing = 0;
                var previousScore = -9999.00;

                rankings.OrderByDescending(x => x.Value).ToList().ForEach(c =>
                {
                    if (previousScore != c.Value)
                        placing += 1;

                    report.Append("<tr>");
                    report.Append("<td>" + placing + "</td><td>" + c.Value + "</td><td>" + c.Key.EntityNumber + "</td><td>" + c.Key.EntityName + " " + c.Key.FirstName + " " + c.Key.LastName + "</td>");
                    report.Append("</tr>");

                    previousScore = c.Value;
                }
                    );   
;
            });

            report.Append("</table>");


            Debug.WriteLine(report.ToString());


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


