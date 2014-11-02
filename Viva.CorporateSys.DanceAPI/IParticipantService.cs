using System.ServiceModel;

namespace Viva.CorporateSys.DanceAPI
{
    [ServiceContract]
    public interface IParticipantService
    {

        [OperationContract]
        Judge GetJudge(string emailAddress);

        [OperationContract]
        Competitor GetCompetitor(string emailAddress);

        [OperationContract]
        Judge AddJudge(Judge judge);

        [OperationContract]
        Organisation AddOrganisation(Organisation organisation);

        [OperationContract]
        Competitor AddCompetitor(Competitor competitor);
    }

    
}