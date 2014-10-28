using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    public class Criterion:BaseObject   
    {
        public Criterion()
        {
            this.Judgings = new HashSet<Judging>();
            this.JudgingAssignments = new HashSet<JudgingAssignment>();
        }

        [Display(Name = "Score Points")]
        public double? ScorePoints { get; set; }
 
        
        //many to many Competition<>Judgings
        public virtual ICollection<Judging> Judgings { get; set; }
        public virtual ICollection<JudgingAssignment> JudgingAssignments { get; set; }
    }
}
