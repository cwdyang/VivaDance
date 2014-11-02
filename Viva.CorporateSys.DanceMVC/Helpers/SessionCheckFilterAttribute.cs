using System.Net;
using System.Web.Mvc;
using Datacom.CorporateSys.Hire.Helpers;
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

        public SessionCheckFilterAttribute()
        {
            _checkCompetitionNotNull = false;
        }

        public SessionCheckFilterAttribute(bool checkExamNotNull)
        {
            _checkCompetitionNotNull = checkExamNotNull;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();


            if (filterContext.Controller is JudgingController)
            {
                var viewModel = filterContext.HttpContext.Session.GetDataFromSession<JudgingViewModel>(SessionConstants.JudgingViewModel);

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
                    }
                }
            }

        }
    }
}