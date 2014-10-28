using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.Dance.Datastore.Mappings
{
    public class JudgeMapping : EntityTypeConfiguration<Judge>
    {
        public JudgeMapping()
        {
            ToTable("Judge", "Dance");

        }
    }
}
