using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Viva.CorporateSys.DanceAPI;

namespace Viva.CorporateSys.DanceMVC.Controllers
{
    public class HomeController : Controller
    {
        private IParticipantService _participantService;

        public HomeController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Viva Latino Dance Judging Panel";

            var user = _participantService.GetJudge(User.Identity.Name);

            if (user != null)
                return RedirectToAction("ActiveCompetitions", "Judging");

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
