using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Viva.CorporateSys.DanceAPI;

namespace Viva.CorporateSys.Dance.ViewModels
{
    public partial class CompetitionViewModel
    {
        public Participant Participant { get; set; }
        public Competition Competition { get; set; }
        public List<Division> Categories { get; set; }

        public CompetitionViewModel(Participant Participant, Competition Competition, List<Division> categories )
        {
            Participant = Participant;
            Competition = Competition;
            Categories = categories;
        }
    }


}