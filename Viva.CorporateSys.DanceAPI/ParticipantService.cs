using AutoMapper;
using Viva.CorporateSys.Dance.Datastore.Repositories;

namespace Viva.CorporateSys.DanceAPI
{
    public class ParticipantService : IParticipantService
    {
        static ParticipantService()
        {
            Mapper.CreateMap<Dance.Domain.Models.Judge, Judge>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.JudgeType, opt => opt.MapFrom(d => d.JudgeType))
                .ForMember(o => o.OrganisationName, opt => opt.MapFrom(d => d.Organisation.Caption))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));

            Mapper.CreateMap<Judge, Dance.Domain.Models.Judge>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.JudgeType, opt => opt.MapFrom(d => d.JudgeType))
                .ForMember(o => o.Organisation.Caption, opt => opt.MapFrom(d => d.OrganisationName))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));

            Mapper.CreateMap<Dance.Domain.Models.Competitor, Competitor>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.CompetitorType, opt => opt.MapFrom(d => d.CompetitorType))
                .ForMember(o => o.OrganisationName, opt => opt.MapFrom(d => d.Organisation.Caption))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));

            Mapper.CreateMap<Competitor, Dance.Domain.Models.Competitor>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.CompetitorType, opt => opt.MapFrom(d => d.CompetitorType))
                .ForMember(o => o.Organisation.Caption, opt => opt.MapFrom(d => d.OrganisationName))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName));

        }

        public Judge AddJudge(Judge judge)
        {
            using (var repo = new ParticipantRepository())
            {
                repo.AddJudge(Mapper.Map<Viva.CorporateSys.Dance.Domain.Models.Judge>(judge));
            }

            return judge;
        }

        public Judge GetJudge(string emailAddress)
        {
            Judge judgeToReturn = null;

            using (var repo = new ParticipantRepository())
            {
                var judge = repo.GetJudge(emailAddress);

                judgeToReturn = Mapper.Map<Judge>(judge);
            }

            return judgeToReturn;
        }


        public Competitor GetCompetitor(string emailAddress)
        {
            Competitor competitorToReturn = null;

            using (var repo = new ParticipantRepository())
            {
                var competitor = repo.GetCompetitor(emailAddress);

                competitorToReturn = Mapper.Map<Competitor>(competitor);
            }

            return competitorToReturn;
        }
    }
}