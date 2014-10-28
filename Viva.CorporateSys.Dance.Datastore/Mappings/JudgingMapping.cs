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
    public class JudgingMapping : EntityTypeConfiguration<Judging>
    {
        public JudgingMapping()
        {

            ToTable("Judging", "Dance");

            //http://forums.asp.net/t/1940806.aspx?EF+tables+causing+cause+cycles+or+multiple+cascade+paths+error+why+
            //one to many Division>Judging
            HasRequired<CompetitorCompetition>(s => s.CompetitorCompetition)
            .WithMany(s => s.Judgings).HasForeignKey(s => s.CompetitorCompetitionId).WillCascadeOnDelete(false);

            //one to many Assignmental JudgingAssignment>Judging
            HasRequired<JudgeCompetition>(e => e.JudgeCompetition)
            .WithMany(s => s.Judgings).HasForeignKey(s => s.JudgeCompetitionId).WillCascadeOnDelete(false);

            //one to many Assignmental JudgingAssignment>Judging
            HasRequired<Criterion>(e => e.Criterion)
            .WithMany(s => s.Judgings).HasForeignKey(s => s.CriterionId);

        }
    }
}
