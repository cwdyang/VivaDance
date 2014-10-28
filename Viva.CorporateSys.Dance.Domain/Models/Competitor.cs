using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    public class Competitor:Participant
    {

        public Competitor()
        {
            this.CompetitorCompetitions = new HashSet<CompetitorCompetition>();
        }

        //lazy loading 
        //one to many Judge/Competitor>CompetitorCompetitions
        public virtual ICollection<CompetitorCompetition> CompetitorCompetitions { get; set; }

        public CompetitorType CompetitorType { get; set; }
    }

    public enum CompetitorType
    {
        Soloist = 0,
        Couple = 1,
        CouplesTeam = 2
    }
}
