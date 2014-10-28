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
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {

            ToTable("Category", "Dance");

            
            //one to many Division>Competition
            HasRequired<Division>(s => s.Division)
            .WithMany(s => s.Categories).HasForeignKey(s => s.DivisionId);

        }
    }
}
