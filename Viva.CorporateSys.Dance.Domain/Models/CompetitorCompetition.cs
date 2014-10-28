using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    /// <summary>
    /// http://www.humanworkflow.net/post/2014/01/30/Managing-Hierarchal-ParentChild-Data-With-DatabaseCode-First-Entity-Framework-And-Recursion.aspx
    /// </summary>
    public class CompetitorCompetition
    {
        public Guid Id { get; set; }

        //one to many Competitor>CompetitorCompetition
        public Guid CompetitorId { get; set; }
        public virtual Competitor Competitor { get; set; }

        //one to many Competition>CompetitorCompetition
        public Guid CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }

        public CompetitorType CompetitorType { get; set; }

        public CompetitorCompetition()
        {
            Judgings = new HashSet<Judging>();
        }

        //lazy loading 
        //one to many Competitor/Competitor>Judgings
        public virtual ICollection<Judging> Judgings { get; set; }
        public int Sequence { get; set; }
    }
}
