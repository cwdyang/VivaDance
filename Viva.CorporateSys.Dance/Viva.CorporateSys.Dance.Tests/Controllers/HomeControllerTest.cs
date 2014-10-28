using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Viva.CorporateSys.Dance.Domain.Models;
using Viva.CorporateSys.DanceAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viva.CorporateSys.Dance;
using Viva.CorporateSys.Dance.Controllers;
using Answer = Viva.CorporateSys.DanceAPI.Answer;
using Division = Viva.CorporateSys.DanceAPI.Division;
using Competition = Viva.CorporateSys.DanceAPI.Competition;


namespace Viva.CorporateSys.Dance.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void GetCompetitionFromAPI()
        {
            var CompetitionService = new CompetitionService();
            var ParticipantService = new ParticipantService();

            var Competition = CompetitionService.GetLatestOpenCompetitionWithJudgingAssignments(ParticipantService.GetParticipant("davidy@Viva.co.nz").Id);

            Competition.Judgings.ToList().ForEach(x => x.Assignments.ToList().ForEach(y =>
            {
                var relatedJudgings = CompetitionService.GetRelatedJudgings(y.Id);

                var answer = new Answer
                {
                    AnswerText = string.Empty,
                    Level = x.Level,
                    ScorePoint = x.ScorePoint,
                    Id = Guid.NewGuid(),
                    
                    //need to use DbContext.JudgingsAssignment.Attach() if you want to use this
                    Assignment = new Assignment {Id = y.Id,IsSelected = y.IsSelected},
                    Competition = new Competition { Id = Competition.Id, Text = Competition.Text }
                };

                var answerReturned = CompetitionService.AddAnswer(answer);

                Debug.WriteLine(relatedJudgings.Count);
            }));

            Assert.IsNotNull(Competition);
        }

        [TestMethod]
        public void GetCategories()
        {
            var CompetitionService = new CompetitionService();

            var categories = CompetitionService.GetCategories(Enumerable.Empty<Guid>().ToList());
        }
    }


     
}
