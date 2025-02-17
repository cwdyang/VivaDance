﻿using System;
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
            try
            {
                foreach (var key in items.AllKeys.Where(x => !x.StartsWith("submit")))
                {
                    var value = Request.Form[key];
                    var valueDouble = 0.0;

                    double.TryParse(value, out valueDouble);

                    var judging = new Judging
                    {
                        CompetitorCompetition = ViewModel.ActiveCompetitorCompetition,
                        JudgeCompetition = ViewModel.ActiveJudgeCompetition,
                        Id = Guid.NewGuid(),
                        Criterion = ViewModel.AllowedCriteria.FirstOrDefault(x => x.Id.ToString() == key),
                        ScorePoints = valueDouble
                    };

                    /*
                    var isJudgingComplete = _competitionService.IsJudgingCompleteForCompetitor(ViewModel.ActiveCompetitorCompetition.Id,
                        ViewModel.ActiveCompetitorCompetition.Competitor.Id, ViewModel.ActiveJudgeCompetition.Judge.Id);
                    */

                    if (judging.Criterion == null)
                        return RedirectToAction("ActiveCompetitions", "Judging");

                    //if (!isJudgingComplete)
                    _competitionService.SubmitJudging(judging);
                }
            }
            catch(Exception ex)
            {

            }
            

            return RedirectToAction("ActiveCompetitions", "Judging");
        }

        [SessionCheckFilter]
        public ActionResult ActiveCompetitions()
        {
            if(ViewModel!=null)
            /*
            if (ViewModel.Competitions == null || 
                !ViewModel.Competitions.Any(x=>new List<CompetitionStatus>{
                    CompetitionStatus.Created,
                    CompetitionStatus.JudgingCompleted,
                    CompetitionStatus.JudgingStarted,
                    CompetitionStatus.JudgingSubmissionCompleted}.Contains(x.CompetitionStatus)))
             */
            {

                if(ViewModel.ActiveCompetition!=null)
                    return View(ViewModel);

                var comps = _competitionService.GetOpenCompetitionsForJudge(ViewModel.Judge.Id).OrderBy(x => x.StartedOn);

                if(!comps.Any())
                    return RedirectToAction("JudgingComplete", "Judging");

                ViewModel.Competitions = comps.ToList();

                ViewModel.ActiveCompetition =
                        ViewModel.Competitions.Where(c => c.CompetitorCompetitions.Any(
                            x => x.Judgings.All(y => y.JudgeCompetition.Judge.Id != ViewModel.Judge.Id)))
                            .OrderBy(x => x.StartedOn)
                            .FirstOrDefault();

                if (ViewModel.ActiveCompetition != null)
                {
                    ViewModel.ActiveCompetitorCompetition =
                        ViewModel.ActiveCompetition.CompetitorCompetitions.Where(
                            x => x.Judgings.All(y => y.JudgeCompetition.Judge.Id != ViewModel.Judge.Id))
                            .OrderBy(x => x.Sequence)
                            .FirstOrDefault();
                }

                if (ViewModel.ActiveCompetitorCompetition==null)
                    return RedirectToAction("JudgingComplete", "Judging");

                ViewModel.ActiveJudgeCompetition =
                    ViewModel.ActiveCompetition.JudgeCompetitions.FirstOrDefault(x => x.Judge.Id == ViewModel.Judge.Id);

                ViewModel.AllowedCriteria = _competitionService.GetAllowedCriteriaForJudge(ViewModel.ActiveCompetition.Id, ViewModel.Judge.Id);
;
            }

            return View(ViewModel);
        }

        [SessionCheckFilterAttribute]
        public ActionResult JudgingResults(Guid? competitionId, Guid? competitorId, string reset)
        {
            var viewModel = new JudgingResultViewModel();

            try
            {
               
                var openCompetitions = _competitionService.GetNotClosedCompetitions().OrderBy(x => x.StartedOn);

                viewModel.ActiveCompetitionListOnly = openCompetitions.ToList();
                viewModel.ActiveCompetitorListOnly = openCompetitions.Select(x => x.CompetitorCompetitions.Select(y => y.Competitor)).SelectMany(y => y).Distinct().ToList();



                var query = openCompetitions.AsQueryable();



                query = (competitionId == null || competitionId == Guid.Empty) ? query : query.Where(x => x.Id == competitionId);
                query = (competitorId == null || competitorId == Guid.Empty) ? query : query.Where(x => x.CompetitorCompetitions.Any(y => y.Competitor.Id == competitorId));
                query = ((competitionId == null || competitionId == Guid.Empty) && (competitorId == null || competitorId == Guid.Empty)) ? openCompetitions.AsQueryable().Where(x => x.Id == Guid.Empty) : query;

                viewModel.ActiveCompetitions = query.ToList();
                viewModel.Criteria = _competitionService.GetAllCriteria().OrderBy(x => x.DisplaySequence).ToList();

            }
            catch (Exception ex)
            {

            }
            
            return View(viewModel);
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
