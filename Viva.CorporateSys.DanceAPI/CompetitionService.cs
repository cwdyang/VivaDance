using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AutoMapper;
using Ninject;
using Viva.CorporateSys.Dance.Datastore.Repositories;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.DanceAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CompetitionService :BaseService, ICompetitionService
    {
        private ICompetitionRepository GetCompetitionRepository()
        {
            return Kernel.Get<ICompetitionRepository>();
        }

        

        static CompetitionService()
        {

            Mapper.CreateMap<Dance.Domain.Models.JudgingAssignment, JudgingAssignment>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Criterion, opt => opt.MapFrom(d => d.Criterion))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption));

            Mapper.CreateMap<JudgingAssignment, Dance.Domain.Models.JudgingAssignment>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Criterion, opt => opt.MapFrom(d => d.Criterion))
                .ForMember(o => o.Caption, opt => opt.MapFrom(d => d.Text));

            Mapper.CreateMap<Dance.Domain.Models.Judging, Judging>()
                .ForMember(o => o.ScorePoints, opt => opt.MapFrom(d => d.ScorePoints))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption))
                .ForMember(o => o.IsExcluded, opt => opt.MapFrom(d => d.IsExcluded))
                .ForMember(o => o.CompetitorCompetition, opt => opt.MapFrom(d => d.CompetitorCompetition))
                .ForMember(o => o.JudgeCompetition, opt => opt.MapFrom(d => d.JudgeCompetition))
                .ForMember(o => o.Criterion, opt => opt.MapFrom(d => d.Criterion))
                .ForMember(o => o.GroupByCompetitorCriterion, opt => opt.ResolveUsing<GroupByCompetitorCriterionResolver>())
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id));

            Mapper.CreateMap<Judging, Dance.Domain.Models.Judging>()
                .ForMember(o => o.ScorePoints, opt => opt.MapFrom(d => d.ScorePoints))
                .ForMember(o => o.Caption, opt => opt.MapFrom(d => d.Text))
                .ForMember(o => o.IsExcluded, opt => opt.MapFrom(d => d.IsExcluded))
                .ForMember(o => o.CompetitorCompetition, opt => opt.MapFrom(d => d.CompetitorCompetition))
                .ForMember(o => o.JudgeCompetition, opt => opt.MapFrom(d => d.JudgeCompetition))
                .ForMember(o => o.Criterion, opt => opt.MapFrom(d => d.Criterion))
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id));
           

            Mapper.CreateMap<Dance.Domain.Models.Judge, Judge>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.JudgeType, opt => opt.MapFrom(d => d.JudgeType))
                .ForMember(o => o.JudgeCompetitions, opt => opt.MapFrom(d => d.JudgeCompetitions))
                .ForMember(o => o.OrganisationName, opt => opt.MapFrom(d => d.Organisation.Caption))
                .ForMember(o => o.OrganisationId, opt => opt.MapFrom(d => d.OrganisationId))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));

            Mapper.CreateMap<Judge, Dance.Domain.Models.Judge>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.JudgeType, opt => opt.MapFrom(d => d.JudgeType))
                .ForMember(o => o.JudgeCompetitions, opt => opt.MapFrom(d => d.JudgeCompetitions))
                .ForMember(o => o.OrganisationId, opt => opt.MapFrom(d => d.OrganisationId))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));

            Mapper.CreateMap<Dance.Domain.Models.Competitor, Competitor>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.CompetitorType, opt => opt.MapFrom(d => d.CompetitorType))
                .ForMember(o => o.EntityNumber, opt => opt.MapFrom(d => d.EntityNumber))
                .ForMember(o => o.EntityName, opt => opt.MapFrom(d => d.EntityName))
                .ForMember(o => o.OrganisationName, opt => opt.MapFrom(d => d.Organisation.Caption))
                .ForMember(o => o.OrganisationId, opt => opt.MapFrom(d => d.OrganisationId))
                .ForMember(o => o.CompetitorCompetitions, opt => opt.MapFrom(d => d.CompetitorCompetitions))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));

            Mapper.CreateMap<Competitor, Dance.Domain.Models.Competitor>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.CompetitorType, opt => opt.MapFrom(d => d.CompetitorType))
                .ForMember(o => o.EntityNumber, opt => opt.MapFrom(d => d.EntityNumber))
                .ForMember(o => o.EntityName, opt => opt.MapFrom(d => d.EntityName))
                .ForMember(o => o.OrganisationId, opt => opt.MapFrom(d => d.OrganisationId))
                .ForMember(o => o.CompetitorCompetitions, opt => opt.MapFrom(d => d.CompetitorCompetitions))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));



            Mapper.CreateMap<CompetitorCompetition, Dance.Domain.Models.CompetitorCompetition>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.CompetitorType, opt => opt.MapFrom(d => d.CompetitorType))
                .ForMember(o => o.Competitor, opt => opt.MapFrom(d => d.Competitor))
                .ForMember(o => o.CompetitionId, opt => opt.MapFrom(d => d.CompetitionId))
                .ForMember(o => o.Sequence, opt => opt.MapFrom(d => d.Sequence));


            Mapper.CreateMap<Dance.Domain.Models.CompetitorCompetition, CompetitorCompetition>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.CompetitorType, opt => opt.MapFrom(d => d.CompetitorType))
                .ForMember(o => o.Competitor, opt => opt.MapFrom(d => d.Competitor))
                .ForMember(o => o.CompetitionId, opt => opt.MapFrom(d => d.CompetitionId))
                .ForMember(o => o.Sequence, opt => opt.MapFrom(d => d.Sequence));

            Mapper.CreateMap<JudgeCompetition, Dance.Domain.Models.JudgeCompetition>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.JudgeType, opt => opt.MapFrom(d => d.JudgeType))
                .ForMember(o => o.CompetitionId, opt => opt.MapFrom(d => d.CompetitionId))
                .ForMember(o => o.Judge, opt => opt.MapFrom(d => d.Judge));


            Mapper.CreateMap<Dance.Domain.Models.JudgeCompetition, JudgeCompetition>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.JudgeType, opt => opt.MapFrom(d => d.JudgeType))
                .ForMember(o => o.CompetitionId, opt => opt.MapFrom(d => d.CompetitionId))
                .ForMember(o => o.Judge, opt => opt.MapFrom(d => d.Judge));


            Mapper.CreateMap<Criterion, Dance.Domain.Models.Criterion>()
               .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
               .ForMember(o => o.Caption, opt => opt.MapFrom(d => d.Text))
               .ForMember(o => o.ScorePoints, opt => opt.MapFrom(d => d.ScorePoints))
               .ForMember(o => o.DisplaySequence, opt => opt.MapFrom(d => d.DisplaySequence));

            Mapper.CreateMap<Dance.Domain.Models.Criterion, Criterion>()
              .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
              .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption))
              .ForMember(o => o.ScorePoints, opt => opt.MapFrom(d => d.ScorePoints))
              .ForMember(o => o.DisplaySequence, opt => opt.MapFrom(d => d.DisplaySequence));


            Mapper.CreateMap<Dance.Domain.Models.Competition,Competition>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.CategoryName, opt => opt.MapFrom(d => d.Category.Caption))
                .ForMember(o => o.DivisionName, opt => opt.MapFrom(d => d.Category.Division.Caption))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Name))
                .ForMember(o => o.Requirements, opt => opt.MapFrom(d => d.Category.Requirements))
                .ForMember(o => o.Location, opt => opt.MapFrom(d => d.Location))
                .ForMember(o => o.GroupComp, opt => opt.MapFrom(d => d.GroupComp))
                .ForMember(o => o.JudgeCompetitions, opt => opt.MapFrom(d => d.JudgeCompetitions))
                .ForMember(o => o.CompetitorCompetitions, opt => opt.MapFrom(d => d.CompetitorCompetitions))
                .ForMember(o => o.CompetitionStatus, opt => opt.MapFrom(d => d.CompetitionStatus))
                .ForMember(o => o.JudgingsRecorded, opt => opt.ResolveUsing<JudgingsRecordedResolver>())
                .ForMember(o => o.JudgingsTotalRequired, opt => opt.ResolveUsing<JudgingsTotalRequiredResolver>())
                .ForMember(o => o.CreatedOn, opt => opt.MapFrom(d => d.CreatedOn))
                .ForMember(o => o.StartedOn, opt => opt.MapFrom(d => d.StartedOn));


            Mapper.CreateMap<Competition, Dance.Domain.Models.Competition>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                //.ForMember(o => o.Category.Caption, opt => opt.MapFrom(d => d.CategoryName))
                //.ForMember(o => o.Category.Division.Caption, opt => opt.MapFrom(d => d.DivisionName))
                .ForMember(o => o.Name, opt => opt.MapFrom(d => d.Text))
                //.ForMember(o => o.Category.Requirements, opt => opt.MapFrom(d => d.Requirements))
                .ForMember(o => o.Location, opt => opt.MapFrom(d => d.Location))
                 .ForMember(o => o.Location, opt => opt.MapFrom(d => d.Location))
                .ForMember(o => o.JudgeCompetitions, opt => opt.MapFrom(d => d.JudgeCompetitions))
                .ForMember(o => o.CompetitorCompetitions, opt => opt.MapFrom(d => d.CompetitorCompetitions))
                .ForMember(o => o.CompetitionStatus, opt => opt.MapFrom(d => d.CompetitionStatus))
                .ForMember(o => o.CreatedOn, opt => opt.MapFrom(d => d.CreatedOn))
                .ForMember(o => o.StartedOn, opt => opt.MapFrom(d => d.StartedOn));

            
        }




        public Competition CompleteCompetition(Competition competition,Judge judge)
        {
            var message = new StringBuilder();



            using (var CompetitionRepo = GetCompetitionRepository())
            {
               
            }


            sendMail(competition.Text, "davidy@Viva.co.nz",
                "Interview Competition results for: " + judge.FirstName + " " + judge.LastName + " Email: " + judge.Email, 
                message.ToString());

            return competition;
        }

        public static void sendMail(string to, string from, string subject, string body)
        {
            var msg = new System.Net.Mail.MailMessage(from, to);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;
            msg.IsBodyHtml = true;
            System.Net.Mail.SmtpClient oSmtpClient = new System.Net.Mail.SmtpClient("dnzakex2.Viva.co.nz");
            //https://social.technet.microsoft.com/Forums/en-US/1a84a06a-f1c8-40b4-ace8-1e264f218aa1/550-571-unable-to-relay-for?forum=exchangesvrsecuremessaginglegacy
            oSmtpClient.UseDefaultCredentials = true;
            oSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //550 5.7.1 Unable to relay for
            oSmtpClient.Send(msg);
        }

        public List<Competition> GetLatestOpenCompetitionForJudge(Guid judgeId)
        {
            var listToReturn = Enumerable.Empty<Competition>().ToList();

            using (var repo = GetCompetitionRepository())
            {
                var comp = repo.GetLatestOpenCompetitionForJudge(judgeId);

                if (comp != null)
                    listToReturn.Add(Mapper.Map<Competition>(comp));
            }
            return listToReturn;
        }
        public List<Competition> GetOpenCompetitionsForJudge(Guid judgeId)
        {
            var listToReturn = Enumerable.Empty<Competition>().ToList();
            var startTime = DateTime.Now;

            using (var repo = GetCompetitionRepository())
            {
                var list = repo.GetOpenCompetitionsForJudge( judgeId);
                var endTime = DateTime.Now;
#if DEBUG
                System.Diagnostics.Debug.WriteLine(string.Format("time take to get opencompetitions {0} ms", (endTime - startTime).Milliseconds));
#endif
                listToReturn = Mapper.Map<List<Competition>>(list);
            }

            return listToReturn;
        }

        public bool IsJudgingCompleteForCompetition(Guid competitionId, Guid? judgeId)
        {
            using (var repo = GetCompetitionRepository())
            {
                return repo.IsJudgingCompleteForCompetition(competitionId, judgeId);
            }
        }

        public bool IsJudgingCompleteForCompetitor(Guid competitionId,Guid competitorId, Guid? judgeId)
        {
            using (var repo = GetCompetitionRepository())
            {
                return repo.IsJudgingCompleteForCompetitor(competitionId,competitorId, judgeId);
            }
        }

        public List<Criterion> GetAllowedCriteriaForJudge(Guid competitionId, Guid judgeId)
        {
            var listToReturn = Enumerable.Empty<Criterion>().ToList();

            using (var repo = GetCompetitionRepository())
            {
                var list = repo.GetAllowedCriteriaForJudge(competitionId, judgeId);

                listToReturn = Mapper.Map<List<Criterion>>(list);
            }

            return listToReturn;
        }

        public List<Competitor> GetCompetitorsForCompetition(Guid competitionId)
        {
            var listToReturn = Enumerable.Empty<Competitor>().ToList();

            using (var repo = GetCompetitionRepository())
            {
                var list = repo.GetCompetitorsForCompetition(competitionId);

                listToReturn = Mapper.Map<List<Competitor>>(list);
            }

            return listToReturn;
        }

        public List<Judge> GetJudgesForCompetition(Guid competitionId)
        {
            var listToReturn = Enumerable.Empty<Judge>().ToList();

            using (var repo = GetCompetitionRepository())
            {
                var list = repo.GetJudgesForCompetition(competitionId);

                listToReturn = Mapper.Map<List<Judge>>(list);
            }

            return listToReturn;
        }

        public List<Criterion> GetAllCriteria()
        {
            var listToReturn = Enumerable.Empty<Criterion>().ToList();

            using (var repo = GetCompetitionRepository())
            {
                var list = repo.GetAllCriteria();

                listToReturn = Mapper.Map<List<Criterion>>(list);
            }

            return listToReturn;
        }

        public List<Judging> GetJudgingsForCompetitor(Guid competitionId, Guid competitorId)
        {
            var listToReturn = Enumerable.Empty<Judging>().ToList();

            using (var repo = GetCompetitionRepository())
            {
                var list = repo.GetJudgingsForCompetitor(competitionId, competitorId);

                listToReturn = Mapper.Map<List<Judging>>(list);
            }

            return listToReturn;
        }

        public Judging SubmitJudging(Judging judging)
        {

            var startTime = DateTime.Now;



            using (var repo = GetCompetitionRepository())
            {
                var judgingToReturn = repo.SubmitJudging(Mapper.Map<Dance.Domain.Models.Judging>(judging));

                var endTime = DateTime.Now;

#if DEBUG
                System.Diagnostics.Debug.WriteLine(string.Format("time take to submit judging {0} ms", (endTime - startTime).Milliseconds));
#endif

                return Mapper.Map<Judging>(judgingToReturn);
            } 
        }

        public void ClearAllJudgings()
        {
            using (var repo = GetCompetitionRepository())
            {
                repo.ClearAllJudgings();
            }
        }

        public List<Competition> GetNotClosedCompetitions()
        {
            var listToReturn = Enumerable.Empty<Competition>().ToList();

            using (var repo = GetCompetitionRepository())
            {
                var list = repo.GetNotClosedCompetitions();

                listToReturn = Mapper.Map<List<Competition>>(list);
            }

            return listToReturn;
        }

        public List<Competition> GetOpenCompetitions()
        {
            var listToReturn = Enumerable.Empty<Competition>().ToList();

            using (var repo = GetCompetitionRepository())
            {
                var list = repo.GetOpenCompetitions();

                listToReturn = Mapper.Map<List<Competition>>(list);
            }

            return listToReturn;
        }
    }

}
