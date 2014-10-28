using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.Dance.Datastore.Mappings
{
    public class JudgingAssignmentMapping:EntityTypeConfiguration<JudgingAssignment>
    {
        public JudgingAssignmentMapping()
        {
            ToTable("JudgingAssignment", "Dance");

            //must specify this or annotate the property
            HasKey(k => new { k.Id });


            //this is shared with Division and Judging
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            //one to many Assignmental JudgingAssignment>Judging
            HasRequired<Criterion>(e => e.Criterion)
            .WithMany(s => s.JudgingAssignments).HasForeignKey(s => s.CriterionId);

            //one to many Judge>Judging
            HasRequired<Judge>(s => s.Judge)
            .WithMany(s => s.JudgingAssignments).HasForeignKey(s => s.JudgeId);

        }
    }
}
