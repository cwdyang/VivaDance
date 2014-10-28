using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    public class Judge:Participant
    {

        public Judge()
        {
            JudgingAssignments = new HashSet<JudgingAssignment>();
            this.JudgeCompetitions = new HashSet<JudgeCompetition>();
        }

        //lazy loading 
        //one to many Judge/Competitor>Competitions
        public virtual ICollection<JudgeCompetition> JudgeCompetitions { get; set; }

        public JudgeType JudgeType { get; set; }

        //one to many Judge>JudgingAssignment
        public virtual ICollection<JudgingAssignment> JudgingAssignments { get; set; }
    }

    public enum JudgeType
    {
        Normal = 0,
        Head = 1
    }
}
