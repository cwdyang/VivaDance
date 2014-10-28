using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    /// <summary>
    /// http://www.humanworkflow.net/post/2014/01/30/Managing-Hierarchal-ParentChild-Data-With-DatabaseCode-First-Entity-Framework-And-Recursion.aspx
    /// </summary>
    public class Judging:BaseObject
    {

        //one to many Criterion>Judging
        public Guid CriterionId { get; set; }
        public virtual Criterion Criterion { get; set; }

        //one to many JudgeCompetition>Judging
        public Guid JudgeCompetitionId { get; set; }
        public virtual JudgeCompetition JudgeCompetition { get; set; }

        //one to many CompetitorCompetition>Judging
        public Guid CompetitorCompetitionId { get; set; }
        public virtual CompetitorCompetition CompetitorCompetition { get; set; }

        public Judging()
        {

        }


        [Display(Name = "Score excluded")]
        public bool IsExcluded { get; set; }


        [DefaultValue(1)]
        [Display(Name = "Score / Point")]
        public double ScorePoints { get; set; }

       


    }
}
