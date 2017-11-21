using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Text;

namespace Viva.CorporateSys.DanceAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Viva.CorporateSys.DanceAPI.ContractType".
    [DataContract]
    [KnownType(typeof(Competition))]
    [KnownType(typeof(JudgingAssignment))]
    [KnownType(typeof(Judge))]
    [KnownType(typeof(Judging))]
    [KnownType(typeof(Competitor))]
    [KnownType(typeof(Criterion))]  
    [KnownType(typeof(CompetitorCompetition))]
    [KnownType(typeof(JudgeCompetition))]
    public class BaseObject
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Text { get; set; }
    }

    [DataContract]
    [KnownType(typeof(Judge))]
    [KnownType(typeof(Competitor))]
    public class Partipant : BaseObject
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public virtual IList<Competition> Competitions { get; set; }

        [DataMember]
        public string MobileNumber { get; set; }

        [DataMember]
        public string OrganisationName { get; set; }

        [DataMember]
        public Guid OrganisationId{ get; set; }
    }

    [DataContract]
    public class Judge : Partipant
    {
        

        [DataMember]
        public JudgeType JudgeType { get; set; }

        [DataMember]
        public ICollection<JudgeCompetition> JudgeCompetitions { get; set; }

    }

   
    [DataContract]
    public enum JudgeType
    {
        Head=0,
        Normal=1
    }

    [DataContract]
    public class Competitor : Partipant
    {
        
        [DataMember]
        public CompetitorType CompetitorType { get; set; }
       
        [DataMember]
        public ICollection<CompetitorCompetition> CompetitorCompetitions { get; set; }

        [DataMember]
        public int EntityNumber { get; set; }

        [DataMember]
        public string EntityName { get; set; }

        [DataMember]
        public string EntityNumberName { get {
            return ((EntityNumber==0)?"":EntityNumber.ToString()) + " " + EntityName;
        } }
    }

    [DataContract]
    public enum CompetitorType
    {
        Soloist = 0,
        Couple = 1,
        CouplesTeam = 2
    }

    [DataContract]
    public class Criterion : BaseObject
    {
        [DataMember]
        public double? ScorePoints { get; set; }

        [DataMember]
        public int DisplaySequence { get; set; }
    }

    [DataContract]
    public class Competition:BaseObject
    {

        public Competition()
        {
            JudgeCompetitions = new HashSet<JudgeCompetition>();
            CompetitorCompetitions = new HashSet<CompetitorCompetition>();
        }

        [DataMember]
        public string CompetitionName { get {
            return DivisionName + " " + CategoryName + " " + ((StartedOn==null)?"":StartedOn.Value.ToString("yyyy MMM dd hh:mm tt"));
        } }

        [DataMember]
        public string CompetitionDropdownName
        {
            get
            {
                return CompetitionName + " Judging Progress:" + this.JudgingsRecorded + "/" + this.JudgingsTotalRequired;
            }
        }

        [DataMember]
        public int JudgingsTotalRequired { get; set; }

        [DataMember]
        public int JudgingsRecorded { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public string DivisionName { get; set; }

        [DataMember]
        public DateTimeOffset CreatedOn { get; set; }

        [DataMember]
        public DateTimeOffset? StartedOn { get; set; }

        [DataMember]
        public DateTimeOffset? CompletedOn { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public byte? GroupComp { get; set; }

        [DataMember]
        public string Requirements { get; set; }

        [DataMember]
        public ICollection<JudgeCompetition> JudgeCompetitions { get; set; }

        [DataMember]
        public ICollection<CompetitorCompetition> CompetitorCompetitions { get; set; }

        [DataMember]
        public CompetitionStatus CompetitionStatus { get; set; }
    }

    [DataContract]
    public enum CompetitionStatus
    {
        Created = 0,
        JudgingStarted = 1,
        JudgingSubmissionCompleted = 2,
        JudgingCompleted = 3,
        Closed = 4
    }

    [DataContract]
    public class JudgingAssignment:BaseObject
    {
        [DataMember]
        public Criterion Criterion { get; set; }
    }

    [DataContract]
    public class Organisation : BaseObject
    {
    }


    [DataContract]
    public class Judging:BaseObject
    {

        [DataMember]
        public bool IsExcluded { get; set; }

        [DataMember]
        public double ScorePoints { get; set; }

        [DataMember]
        public Criterion Criterion { get; set; }

        [DataMember]
        public CompetitorCompetition CompetitorCompetition { get; set; }

        [DataMember]
        public JudgeCompetition JudgeCompetition { get; set; }


        [DataMember]
        public string GroupByCompetitorCriterion {
            get
            {
                var value = "";
                return value;

                try
                {
                    value = this.CompetitorCompetition.Competitor.Id.ToString() + this.Criterion.Id.ToString();
                }
                catch (Exception)
                {

                }

                
            }
        }
    }


    [DataContract]
    [KnownType(typeof(CompetitorCompetition))]
    [KnownType(typeof(JudgeCompetition))]
    public class ParticipantCompetition : BaseObject
    {
        [DataMember]
        public ICollection<Judging> Judgings { get; set; }

        [DataMember]
        public Guid CompetitionId { get; set; }
    }



    [DataContract]
    public class CompetitorCompetition : ParticipantCompetition
    {
        [DataMember]
        public CompetitorType CompetitorType { get; set; }

        [DataMember]
        public Competitor Competitor { get; set; }

        [DataMember]
        public int Sequence { get; set; }

        [DataMember]
        public double TotalScorePoints { get; set; }

        [DataMember]
        public double TotalPenaltyPoints { get; set; }

        [DataMember]
        public double TotalCombined { get { return TotalScorePoints + TotalPenaltyPoints; } }

        
    }

    [DataContract]
    public class JudgeCompetition : ParticipantCompetition
    {
        [DataMember]
        public JudgeType JudgeType { get; set; }

        [DataMember]
        public Judge Judge { get; set; }

    }

   

}
