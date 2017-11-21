using AutoMapper;
using Ninject;
using System.Collections.Generic;
using Viva.CorporateSys.Dance.Datastore.Repositories;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.DanceAPI
{
    public class ParticipantService  : BaseService, IParticipantService
    {
        private IParticipantRepository GetParticipantRepository()
        {
            return Kernel.Get<IParticipantRepository>();
        }

        static ParticipantService()
        {
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


            Mapper.CreateMap<Dance.Domain.Models.JudgingAssignment, JudgingAssignment>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Criterion, opt => opt.MapFrom(d => d.Criterion))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption));

            Mapper.CreateMap<JudgingAssignment, Dance.Domain.Models.JudgingAssignment>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Criterion, opt => opt.MapFrom(d => d.Criterion))
                .ForMember(o => o.Caption, opt => opt.MapFrom(d => d.Text));

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

            Mapper.CreateMap<Organisation, Dance.Domain.Models.Organisation>()
               .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
               .ForMember(o => o.Caption, opt => opt.MapFrom(d => d.Text));

            Mapper.CreateMap<Dance.Domain.Models.Organisation,Organisation>()
               .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
               .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption));

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

        }

        public Organisation AddOrganisation(Organisation organisation)
        {
            using (var repo = new ParticipantRepository())
            {
                repo.AddOrganisation(Mapper.Map<Viva.CorporateSys.Dance.Domain.Models.Organisation>(organisation));
            }

            return organisation;
        }

        public Judge AddJudge(Judge judge)
        {
            using (var repo = new ParticipantRepository())
            {
                repo.AddJudge(Mapper.Map<Viva.CorporateSys.Dance.Domain.Models.Judge>(judge));
            }

            return judge;
        }


        public Competitor AddCompetitor(Competitor competitor)
        {
            using (var repo = GetParticipantRepository())
            {
                repo.AddCompetitor(Mapper.Map<Viva.CorporateSys.Dance.Domain.Models.Competitor>(competitor));
            }

            return competitor;
        }

        public Judge GetJudge(string emailAddress, bool includeDependencies = false)
        {
            Judge judgeToReturn = null;

            using (var repo = GetParticipantRepository())
            {
                var judge = repo.GetJudge(emailAddress, includeDependencies);

                if (judge == null) return null;

                judgeToReturn = (judge!=null&&!includeDependencies)?new Judge
                {
                    Id = judge.Id,
                    Email = judge.Email,
                    FirstName =judge.FirstName,
                    LastName = judge.FirstName,
                    JudgeType = (JudgeType)judge.JudgeType,
                    MobileNumber = judge.MobileNumber,
                    Competitions = new List<Competition>(),
                    JudgeCompetitions = new List<JudgeCompetition>()
                } :Mapper.Map<Judge>(judge);
            }

            return judgeToReturn;
        }


        public Competitor GetCompetitor(string emailAddress)
        {
            Competitor competitorToReturn = null;

            using (var repo = GetParticipantRepository())
            {
                var competitor = repo.GetCompetitor(emailAddress);

                competitorToReturn = Mapper.Map<Competitor>(competitor);
            }

            return competitorToReturn;
        }
    }
}