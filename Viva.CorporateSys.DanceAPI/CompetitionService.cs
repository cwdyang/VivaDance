using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AutoMapper;
using Viva.CorporateSys.Dance.Datastore.Repositories;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.DanceAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CompetitionService : ICompetitionService
    {
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
                .ForMember(o => o.Criterion, opt => opt.MapFrom(d => d.Criterion))
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id));

            Mapper.CreateMap<Judging, Dance.Domain.Models.Judging>()
                .ForMember(o => o.ScorePoints, opt => opt.MapFrom(d => d.ScorePoints))
                .ForMember(o => o.Caption, opt => opt.MapFrom(d => d.Text))
                .ForMember(o => o.IsExcluded, opt => opt.MapFrom(d => d.IsExcluded))
                .ForMember(o => o.Criterion, opt => opt.MapFrom(d => d.Criterion))
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id));
           

            Mapper.CreateMap<Dance.Domain.Models.Judge, Judge>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.JudgeType, opt => opt.MapFrom(d => d.JudgeType))
                .ForMember(o => o.JudgeCompetitions, opt => opt.MapFrom(d => d.JudgeCompetitions))
                .ForMember(o => o.OrganisationName, opt => opt.MapFrom(d => d.Organisation.Caption))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));

            Mapper.CreateMap<Judge, Dance.Domain.Models.Judge>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.JudgeType, opt => opt.MapFrom(d => d.JudgeType))
                .ForMember(o => o.JudgeCompetitions, opt => opt.MapFrom(d => d.JudgeCompetitions))
                .ForMember(o => o.Organisation.Caption, opt => opt.MapFrom(d => d.OrganisationName))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));

            Mapper.CreateMap<Dance.Domain.Models.Competitor, Competitor>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.CompetitorType, opt => opt.MapFrom(d => d.CompetitorType))
                .ForMember(o => o.OrganisationName, opt => opt.MapFrom(d => d.Organisation.Caption))
                .ForMember(o => o.CompetitorCompetitions, opt => opt.MapFrom(d => d.CompetitorCompetitions))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));

            Mapper.CreateMap<Competitor, Dance.Domain.Models.Competitor>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.CompetitorType, opt => opt.MapFrom(d => d.CompetitorType))
                .ForMember(o => o.Organisation.Caption, opt => opt.MapFrom(d => d.OrganisationName))
                .ForMember(o => o.CompetitorCompetitions, opt => opt.MapFrom(d => d.CompetitorCompetitions))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));



            Mapper.CreateMap<CompetitorCompetition, Dance.Domain.Models.CompetitorCompetition>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.CompetitorType, opt => opt.MapFrom(d => d.CompetitorType))
                .ForMember(o => o.Sequence, opt => opt.MapFrom(d => d.Sequence));


            Mapper.CreateMap<Dance.Domain.Models.CompetitorCompetition, CompetitorCompetition>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.CompetitorType, opt => opt.MapFrom(d => d.CompetitorType))
                .ForMember(o => o.Sequence, opt => opt.MapFrom(d => d.Sequence));

            Mapper.CreateMap<Dance.Domain.Models.Competition,Competition>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.CategoryName, opt => opt.MapFrom(d => d.Category.Caption))
                .ForMember(o => o.DivisionName, opt => opt.MapFrom(d => d.Category.Division.Caption))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Name))
                .ForMember(o => o.Requirements, opt => opt.MapFrom(d => d.Category.Requirements))
                .ForMember(o => o.Location, opt => opt.MapFrom(d => d.Location))
                .ForMember(o => o.JudgeCompetitions, opt => opt.MapFrom(d => d.JudgeCompetitions))
                .ForMember(o => o.CompetitorCompetitions, opt => opt.MapFrom(d => d.CompetitorCompetitions))
                .ForMember(o => o.CompetitionStatus, opt => opt.MapFrom(d => d.CompetitionStatus))
                .ForMember(o => o.CreatedOn, opt => opt.MapFrom(d => d.CreatedOn))
                .ForMember(o => o.StartedOn, opt => opt.MapFrom(d => d.StartedOn));


            Mapper.CreateMap<Competition, Dance.Domain.Models.Competition>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                //.ForMember(o => o.Category.Caption, opt => opt.MapFrom(d => d.CategoryName))
                //.ForMember(o => o.Category.Division.Caption, opt => opt.MapFrom(d => d.DivisionName))
                .ForMember(o => o.Name, opt => opt.MapFrom(d => d.Text))
                //.ForMember(o => o.Category.Requirements, opt => opt.MapFrom(d => d.Requirements))
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

            

            using (var CompetitionRepo = new CompetitionRepository())
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

        public List<Division> GetCategories(List<Guid> DivisionIds)
        {
            var list = Enumerable.Empty<Division>().ToList();

            using (var CompetitionRepo = new CompetitionRepository())
            {
               
            }

            return list;
        }


        public Competition GenerateCompetition(List<Guid> DivisionIds,Guid JudgeGuid,string Competitioniner)
        {
            using (var CompetitionRepo = new CompetitionRepository())
            {
                return null;
            }
        }

        public List<Competition> GetOpenCompetitionsForJudge(Guid judgeId)
        {
            throw new NotImplementedException();
        }

        public bool IsJudgingCompleteForCompetition(Guid competitionId, Guid? judgeId)
        {
            throw new NotImplementedException();
        }

        public bool IsJudgingCompleteForCompetitor(Guid competitionId, Guid? judgeId)
        {
            throw new NotImplementedException();
        }

        public List<Criterion> GetAllowedCriteriaForJudge(Guid competitionId, Guid judgeId)
        {
            throw new NotImplementedException();
        }

        public List<Competitor> GetCompetitorsForCompetition(Guid competitionId)
        {
            throw new NotImplementedException();
        }

        public List<Judge> GetJudgesForCompetition(Guid competitionId)
        {
            throw new NotImplementedException();
        }

        public List<Criterion> GetAllCriteria()
        {
            throw new NotImplementedException();
        }

        public List<Judging> GetJudgingsForCompetitor(Guid competitionId, Guid competitorId)
        {
            throw new NotImplementedException();
        }

        public bool SubmitJudging(Judging judging)
        {
            throw new NotImplementedException();
        }
    }
}
