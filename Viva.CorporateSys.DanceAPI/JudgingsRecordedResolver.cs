using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Viva.CorporateSys.DanceAPI
{
    public class JudgingsRecordedResolver : ValueResolver<Dance.Domain.Models.Competition, int>
    {
        protected override int ResolveCore(Dance.Domain.Models.Competition source)
        {
            return source.CompetitorCompetitions.SelectMany(x => x.Judgings).Count();
        }
    }
}
