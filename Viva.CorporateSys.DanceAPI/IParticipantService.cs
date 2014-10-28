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
    }

    
}