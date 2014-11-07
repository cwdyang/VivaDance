using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Viva.CorporateSys.Dance.Datastore.Repositories;

namespace Viva.CorporateSys.DanceAPI
{
    public class APIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICompetitionRepository>().To<CompetitionRepository>().InTransientScope();
            Bind<IParticipantRepository>().To<ParticipantRepository>().InTransientScope();

            Bind<ICompetitionService>().To<CompetitionService>().InTransientScope();
            Bind<IParticipantService>().To<ParticipantService>().InTransientScope();
        }
    }
}
