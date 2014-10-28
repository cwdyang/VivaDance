using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Viva.CorporateSys.DanceAPI
{
    public class DivisionNameResolver : ValueResolver<Viva.CorporateSys.Dance.Domain.Models.Judging, string>
    {
        protected override string ResolveCore(Dance.Domain.Models.Judging source)
        {
            return "";
        }
    }
}
