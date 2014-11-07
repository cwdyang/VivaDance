using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Datacom.CorporateSys.Hire.Helpers;
using Ninject;
using Viva.CorporateSys.DanceAPI;
using Viva.CorporateSys.DanceMVC.Constants;
using Viva.CorporateSys.DanceMVC.Helpers;
using Viva.CorporateSys.DanceMVC.ViewModels;

namespace Viva.CorporateSys.DanceMVC.Controllers
{
    public class JudgingController : Controller
    {
        private ICompetitionService _competitionService;

        public ICompetitionService CompetitionService
        {
            get { return _competitionService; }
        }

        public JudgingController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        public JudgingController()
        {
           
        }

        public JudgingViewModel ViewModel
        {
            get { return Session.GetDataFromSession<JudgingViewModel>(SessionConstants.JudgingViewModel); }
            set { Session.SetDataToSession<JudgingViewModel>(SessionConstants.JudgingViewModel, value); }
        }

        // GET: Judging
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [SessionCheckFilter]
        public ActionResult SubmitJudgings(FormCollection items)
        {

            foreach (var key in items.AllKeys.Where(x => !x.StartsWith("submit")))
            {
                var value = Request.Form[key];

                var judging = new Judging
                {
                    CompetitorCompetition = ViewModel.ActiveCompetitorCompetition,
                    JudgeCompetition = ViewModel.ActiveJudgeCompetition,
                    Id = Guid.NewGuid(),
                    Criterion = ViewModel.AllowedCriteria.FirstOrDefault(x => x.Id.ToString() == key),
                    ScorePoints = double.Parse(value)
                };

                var isJudgingComplete = _competitionService.IsJudgingCompleteForCompetitor(ViewModel.ActiveCompetitorCompetition.Id,
                    ViewModel.ActiveCompetitorCompetition.Competitor.Id, ViewModel.ActiveJudgeCompetition.Judge.Id);

                //if (!isJudgingComplete)
                _competitionService.SubmitJudging(judging);
            }

            return RedirectToAction("ActiveCompetitions", "Judging");
        }

        [SessionCheckFilter]
        public ActionResult ActiveCompetitions()
        {
            /*
            if (ViewModel.Competitions == null || 
                !ViewModel.Competitions.Any(x=>new List<CompetitionStatus>{
                    CompetitionStatus.Created,
                    CompetitionStatus.JudgingCompleted,
                    CompetitionStatus.JudgingStarted,
                    CompetitionStatus.JudgingSubmissionCompleted}.Contains(x.CompetitionStatus)))
             */
            {

                

                var comps = _competitionService.GetOpenCompetitionsForJudge(ViewModel.Judge.Id).OrderBy(x => x.StartedOn);

                if(!comps.Any())
                    return RedirectToAction("JudgingComplete", "Judging");

                ViewModel.Competitions = comps.ToList();

                ViewModel.ActiveCompetition = comps.First();

                var allowedCriteria = _competitionService.GetAllowedCriteriaForJudge(ViewModel.ActiveCompetition.Id, ViewModel.Judge.Id);

                ViewModel.ActiveCompetitorCompetition =
                    ViewModel.ActiveCompetition.CompetitorCompetitions.Where(
                        x => x.Judgings.All(y => y.JudgeCompetition.Judge.Id != ViewModel.Judge.Id)).OrderBy(x => x.Competitor.EntityNumber).FirstOrDefault();

                if (ViewModel.ActiveCompetitorCompetition==null)
                    return RedirectToAction("JudgingComplete", "Judging");

                ViewModel.ActiveJudgeCompetition =
                    ViewModel.ActiveCompetition.JudgeCompetitions.FirstOrDefault(x => x.Judge.Id == ViewModel.Judge.Id);

                ViewModel.AllowedCriteria = allowedCriteria;
            }

            return View(ViewModel);
        }

        public ActionResult JudgingComplete()
        {

            ViewBag.Message = "Your judging is complete, please hand the equipment back to the organiser.";

            return View(ViewModel);

        }

        // GET: Judging/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Judging/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Judging/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Judging/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Judging/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Judging/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Judging/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
