using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Viva.CorporateSys.Dance.Constants;
using Viva.CorporateSys.Dance.Helpers;
using Viva.CorporateSys.Dance.ViewModels;
using Viva.CorporateSys.DanceAPI;
using WebGrease.Css.Extensions;

namespace Viva.CorporateSys.Dance.Controllers
{
    public class CompetitionController : Controller
    {
        protected CompetitionViewModel ViewModel
        {
            get { return Session.GetDataFromSession<CompetitionViewModel>(SessionConstants.CompetitionViewModel); }
            set { Session.SetDataToSession<CompetitionViewModel>(SessionConstants.CompetitionViewModel, value); }
        }

        //http://blog.stevensanderson.com/2010/01/28/editing-a-variable-length-list-aspnet-mvc-2-style/
        public ActionResult DivisionTree(IEnumerable<Guid> DivisionIds, Guid? ParticipantId, bool recurse=false)
        {

            var categories = _CompetitionService.GetCategories((DivisionIds==null)?Enumerable.Empty<Guid>().ToList(): DivisionIds.ToList());

            if (ViewModel == null || ViewModel.Participant == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewModel.Categories = categories;
            }


            return View(ViewModel);
        }

        public ActionResult GenerateCompetition(FormCollection items)
        {
            var DivisionIds = new List<Guid>();

            foreach (var key in items.AllKeys)
            {
                var value = Request.Form[key];

                if(value.StartsWith("true"))
                    DivisionIds.Add(new Guid(key));
            }

            //TODO
            var Competition = _CompetitionService.GenerateCompetition(DivisionIds, ViewModel.Participant.Id, "davidy@Viva.co.nz");

            return RedirectToAction("Competition", "Competition");
        }

        //for injection
        public CompetitionController(IParticipantService ParticipantService, ICompetitionService CompetitionService)
        {
            _ParticipantService = ParticipantService;
            _CompetitionService = CompetitionService;
        }

        readonly IParticipantService _ParticipantService = new ParticipantService();
        readonly ICompetitionService _CompetitionService = new CompetitionService();

        public CompetitionController()
        {
            _ParticipantService = new ParticipantService();
            _CompetitionService = new CompetitionService();
        }

        public ActionResult Competition(int? JudgingNumber)
        {

            if (ViewModel == null|| ViewModel.Participant == null )
                return RedirectToAction("Login", "Account");

            if( ViewModel.Competition == null)
            {

                var Competition = _CompetitionService.GetLatestOpenCompetitionWithJudgingAssignments(ViewModel.Participant.Id);

                if (Competition == null)
                    return new HttpStatusCodeResult(404); //return new EmptyResult(); //preferred

                Competition.CurrentJudgingId = Competition.Judgings.First().Id;

                ViewModel.Competition = Competition;

            }

            ViewModel.Competition.CurrentJudgingNumber = (JudgingNumber) ?? 1;

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult AnswerJudging(Judging Judging)
        {
            if (ViewModel == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Assignment AssignmentSelected = null;

            try
            {
                AssignmentSelected = Newtonsoft.Json.JsonConvert.DeserializeObject<Assignment>(Judging.SelectedAssignmentJSON);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(404);
            }
            
            var parentJudging = ViewModel.Competition.Judgings.First(x => x.Id == AssignmentSelected.ParentJudgingId);

            var answer = new Answer { AnswerText = AssignmentSelected.Text, Competition = ViewModel.Competition, Id = Guid.NewGuid(), Level = parentJudging.Level, Assignment = AssignmentSelected, ScorePoint = parentJudging.ScorePoint, Text = AssignmentSelected.Text };

            _CompetitionService.AddAnswer(answer);

            parentJudging.SelectedAssignment = AssignmentSelected;

            if (ViewModel.Competition.Judgings.All(x => x.SelectedAssignment != null))
                return CompleteCompetition();

            ViewModel.Competition.CurrentJudgingNumber += (ViewModel.Competition.CurrentJudgingNumber ==
                                                     ViewModel.Competition.Judgings.Count)
                ? 0
                : 1;

            //return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Competition", "Competition", new { JudgingNumber = ViewModel.Competition.CurrentJudgingNumber});
        }

        private ActionResult CompleteCompetition()
        {
            _CompetitionService.CompleteCompetition(ViewModel.Competition, ViewModel.Participant);
            ViewModel.Competition = null;
            ViewModel.Categories = null;
            return RedirectToAction("DivisionTree", "Competition");
        }
    }
}
