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
    public class CompetitionMapping:EntityTypeConfiguration<Competition>
    {
        public CompetitionMapping()
        {

            ToTable("Competition", "Dance");

            //must specify this or annotate the property
            HasKey(k => new { k.Id });

            //this is shared with Division and Judging
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            //one to many Category>Competition
            HasRequired<Category>(s => s.Category)
            .WithMany(s => s.Competitions).HasForeignKey(s => s.CategoryId);



        }
    }
}
