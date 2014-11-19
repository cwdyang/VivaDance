using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Viva.CorporateSys.DanceAPI
{
    class JudgingsTotalRequiredResolver : ValueResolver<Dance.Domain.Models.Competition, int>
    {
        protected override int ResolveCore(Dance.Domain.Models.Competition source)
        {
            return source.JudgeCompetitions.SelectMany(x => x.Judge.JudgingAssignments).Count() * source.CompetitorCompetitions.Count();
        }
    }
}
