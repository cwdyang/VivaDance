using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web.Mvc;
using Datacom.CorporateSys.Hire.Helpers;
using Ninject;
using Viva.CorporateSys.DanceAPI;
using Viva.CorporateSys.DanceMVC.Constants;
using Viva.CorporateSys.DanceMVC.Controllers;
using Viva.CorporateSys.DanceMVC.ViewModels;

namespace Viva.CorporateSys.DanceMVC.Helpers
{
    /// <summary>
    /// you can add this to FilterConfig.RegisterGlobalFilters
    /// </summary>
    public class SessionCheckFilterAttribute:ActionFilterAttribute
    {
        private bool _checkCompetitionNotNull = false;
        private ICompetitionService _competitionService;
        private IParticipantService _participantService;
        public static IKernel Kernel = null;

        static SessionCheckFilterAttribute()
        {
             Kernel = new StandardKernel(new APIModule());
        }

        public SessionCheckFilterAttribute(ICompetitionService competitionService, IParticipantService participantService)
        {
            _competitionService = competitionService;
            _participantService = participantService;
            _checkCompetitionNotNull = false;
        }

        public SessionCheckFilterAttribute()
        {
            _competitionService = Kernel.Get<ICompetitionService>();
            _participantService = Kernel.Get<IParticipantService>();
            _checkCompetitionNotNull = false;
        }

        public SessionCheckFilterAttribute(bool checkExamNotNull)
        {
            _competitionService = Kernel.Get<ICompetitionService>();
            _participantService = new StandardKernel().Get<IParticipantService>();
            _checkCompetitionNotNull = checkExamNotNull;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();


            if (filterContext.Controller is JudgingController)
            {
                var viewModel = filterContext.HttpContext.Session.GetDataFromSession<JudgingViewModel>(SessionConstants.JudgingViewModel);

                var userName = filterContext.RequestContext.HttpContext.User.Identity.Name;

                if (userName != null)
                {

                    var user = _participantService.GetJudge(userName);

                    if (user != null)
                    {

                        switch(filterContext.ActionDescriptor.ActionName)
                        { 
                            case "JudgingResults":
                                if(userName!="administrator")
                                {
                                    filterContext.Result = new EmptyResult();
                                    return;
                                }
                                break;
                            case "ActiveCompetitions":
                            viewModel = new JudgingViewModel(user, _competitionService.GetOpenCompetitionsForJudge(user.Id));

                            viewModel.ActiveCompetition = 
                            viewModel.Competitions.Where(c => c.CompetitorCompetitions.Any(
                                x => x.Judgings.All(y => y.JudgeCompetition.Judge.Id != viewModel.Judge.Id)))
                                .OrderBy(x => x.StartedOn)
                                .FirstOrDefault();

                            if (viewModel.ActiveCompetition != null)
                            {
                                viewModel.ActiveCompetitorCompetition =
                                    viewModel.ActiveCompetition.CompetitorCompetitions.Where(
                                        x => x.Judgings.All(y => y.JudgeCompetition.Judge.Id != viewModel.Judge.Id))
                                        .OrderBy(x => x.Sequence)
                                        .FirstOrDefault();
                            }


                            if (viewModel.ActiveCompetition==null||
                                viewModel.ActiveCompetitorCompetition == null)
                            {
                                var url = new UrlHelper(filterContext.RequestContext);
                                var urlContent = url.Content("~/Judging/JudgingComplete");
                                filterContext.HttpContext.Response.Redirect(urlContent, true);
                                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Redirect); //STOPS execution!!
                                return;
                            }

                        
                            viewModel.ActiveJudgeCompetition =
                                viewModel.ActiveCompetition.JudgeCompetitions.FirstOrDefault(x => x.Judge.Id == viewModel.Judge.Id);

                            viewModel.AllowedCriteria = _competitionService.GetAllowedCriteriaForJudge(viewModel.ActiveCompetition.Id, viewModel.Judge.Id);
                            filterContext.HttpContext.Session.SetDataToSession<JudgingViewModel>(SessionConstants.JudgingViewModel,  viewModel);
                            return;

                        }
                    }
                }

                var sessionCheckFail = (_checkCompetitionNotNull) ? (viewModel == null || viewModel.Judge == null || viewModel.Competitions == null) : (viewModel == null || viewModel.Competitions == null);

                if (sessionCheckFail || !filterContext.HttpContext.Request.IsAuthenticated)
                {
                    if(filterContext.HttpContext.Request.IsAjaxRequest())
                        filterContext.Result = new EmptyResult();
                    else
                    {
                        var url = new UrlHelper(filterContext.RequestContext);
                        var loginUrl = url.Content("~/Account/Login");
                        filterContext.HttpContext.Response.Redirect(loginUrl, true);
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Redirect); //STOPS execution!!
                        return;
                    }
                }
            }

        }
    }
}