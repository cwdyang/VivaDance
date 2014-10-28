using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    public class JudgingAssignment : BaseObject
    {

        //one to many Judge>JudgingAssignment
        public Guid? JudgeId { get; set; }
        public virtual Judge Judge { get; set; }

        //one to many Criterion>JudgingAssignment
        public Guid? CriterionId { get; set; }
        public virtual Criterion Criterion { get; set; }

        public JudgingAssignment()
        {

        }
    }
}
