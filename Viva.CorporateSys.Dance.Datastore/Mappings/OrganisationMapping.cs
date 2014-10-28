using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.Dance.Datastore.Mappings
{
    public class OrganisationMapping:EntityTypeConfiguration<Organisation>
    {
        public OrganisationMapping()
        {
            ToTable("Organisation", "Dance");

            HasKey(k => new { k.Id });

            //this is shared with Division and Judging
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }
    }
}
