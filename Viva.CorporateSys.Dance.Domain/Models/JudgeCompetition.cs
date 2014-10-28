using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    /// <summary>
    /// http://www.humanworkflow.net/post/2014/01/30/Managing-Hierarchal-ParentChild-Data-With-DatabaseCode-First-Entity-Framework-And-Recursion.aspx
    /// </summary>
    public class JudgeCompetition
    {
        public Guid Id { get; set; }

        //one to many judge>JudgeCompetition
        public Guid JudgeId { get; set; }
        public virtual Judge Judge { get; set; }

        //one to many Competition>JudgeCompetition
        public Guid CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }

        public JudgeType JudgeType { get; set; }

        public JudgeCompetition()
        {
            Judgings = new HashSet<Judging>();
        }

        //lazy loading 
        //one to many Judge/Competitor>Judgings
        public virtual ICollection<Judging> Judgings { get; set; }

    }
}
