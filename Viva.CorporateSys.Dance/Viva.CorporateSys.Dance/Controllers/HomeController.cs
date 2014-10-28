using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Viva.CorporateSys.Dance.ViewModels;
using Viva.CorporateSys.DanceAPI;

namespace Viva.CorporateSys.Dance.Controllers
{
    public class HomeController : CompetitionController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome!";

            if (User.Identity.IsAuthenticated && (ViewModel==null||ViewModel.Participant == null))
                return RedirectToAction("LogOut", "Account");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About...";

            return View();
        }

        
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact...";

            return View();
        }
    }
}
