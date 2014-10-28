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
    public class JudgeCompetitionMapping : EntityTypeConfiguration<JudgeCompetition>
    {
        public JudgeCompetitionMapping()
        {

            ToTable("JudgeCompetition", "Dance");

            //must specify this or annotate the property
            HasKey(k => new { k.Id });

            //this is shared with Division and Judging
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            //one to many Competitor>CompetitorCompetition
            HasRequired<Judge>(s => s.Judge)
            .WithMany(s => s.JudgeCompetitions).HasForeignKey(s => s.JudgeId);

            //one to many Competition>CompetitorCompetition
            HasRequired<Competition>(s => s.Competition)
            .WithMany(s => s.JudgeCompetitions).HasForeignKey(s => s.CompetitionId);

        }
    }
}
