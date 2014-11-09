using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Viva.CorporateSys.DanceAPI;

namespace Viva.CorporateSys.DanceMVC.ViewModels
{
    public class JudgingResultViewModel
    {
        public List<Competition> ActiveCompetitions { get; set; }
        public List<Criterion> Criteria { get; set; }
    }
}