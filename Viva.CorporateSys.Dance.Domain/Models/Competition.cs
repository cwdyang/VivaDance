using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    public class Competition
    {

        public Guid Id { get; set; }

        

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name = "Created On")]
        public DateTimeOffset CreatedOn { get; set; }

        [Display(Name = "Started On")]
        public DateTimeOffset? StartedOn { get; set; }

        [Display(Name = "Completed On")]
        public DateTimeOffset? CompletedOn { get; set; }

        [StringLength(255)]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [StringLength(255)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public byte? GroupComp { get; set; }

        public Competition()
        {
            this.JudgeCompetitions = new HashSet<JudgeCompetition>();
            this.CompetitorCompetitions = new HashSet<CompetitorCompetition>();

        }

        //many to many Competition<>JudgeCompetition
        public virtual ICollection<JudgeCompetition> JudgeCompetitions { get; set; }

        //many to many Competition<>CompetitorCompetitions
        public virtual ICollection<CompetitorCompetition> CompetitorCompetitions { get; set; }

        public CompetitionStatus CompetitionStatus { get; set; }
        
    }

    public enum CompetitionStatus
    {
        Created = 0,
        JudgingStarted = 1,
        JudgingSubmissionCompleted = 2,
        JudgingCompleted = 3,
        Closed = 4
    }
}
