using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Datacom.CorporateSys.Hire.Helpers;
using Viva.CorporateSys.DanceAPI;
using Viva.CorporateSys.DanceMVC.Constants;
using Viva.CorporateSys.DanceMVC.Helpers;
using Viva.CorporateSys.DanceMVC.ViewModels;

namespace Viva.CorporateSys.DanceMVC.Controllers
{
    public class JudgingController : Controller
    {
        private ICompetitionService _competitionService;

        protected JudgingViewModel ViewModel
        {
            get { return Session.GetDataFromSession<JudgingViewModel>(SessionConstants.JudgingViewModel); }
            set { Session.SetDataToSession<JudgingViewModel>(SessionConstants.JudgingViewModel, value); }
        }

        // GET: Judging
        public ActionResult Index()
        {
            return View();
        }

        [SessionCheckFilter]
        public ActionResult ActiveCompetitions()
        {
            if (ViewModel.Competitions == null || 
                !ViewModel.Competitions.Any(x=>new List<CompetitionStatus>{
                    CompetitionStatus.Created,
                    CompetitionStatus.JudgingCompleted,
                    CompetitionStatus.JudgingStarted,
                    CompetitionStatus.JudgingSubmissionCompleted}.Contains(x.CompetitionStatus)))
            {
                var comps = _competitionService.GetOpenCompetitionsForJudge(ViewModel.Judge.Id).OrderBy(x => x.StartedOn);

                ViewModel.Competitions = comps.ToList();
            }

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
