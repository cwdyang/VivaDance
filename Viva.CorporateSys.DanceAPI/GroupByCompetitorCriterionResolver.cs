using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Viva.CorporateSys.DanceAPI
{
    public class GroupByCompetitorCriterionResolver : ValueResolver<Dance.Domain.Models.Judging, string>
    {
        protected override string ResolveCore(Dance.Domain.Models.Judging source)
        {
            var value = "";
            return value;

            try
            {
                value = source.CompetitorCompetition.Competitor.Id.ToString() + source.Criterion.Id.ToString();
            }
            catch (Exception)
            {
               
            }

            

        }
    }
}
