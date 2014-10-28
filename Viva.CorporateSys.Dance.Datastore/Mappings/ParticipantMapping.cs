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
    public class ParticipantMapping:EntityTypeConfiguration<Participant>
    {
        public ParticipantMapping()
        {
            ToTable("Participant", "Dance");

            HasKey(k => new { k.Id });

            //this is shared with Division and Judging
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //one to many Assignmental JudgingAssignment>Judging    
            HasRequired<Organisation>(e => e.Organisation)
            .WithMany(s => s.Participants).HasForeignKey(s => s.OrganisationId);
        }
    }
}
