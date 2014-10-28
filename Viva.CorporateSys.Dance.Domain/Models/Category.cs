using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    public class Category:BaseObject   
    {
        public Category()
        {
            this.Competitions = new HashSet<Competition>();
        }

        //one to many Category>Competition
        public virtual ICollection<Competition> Competitions { get; set; }

        //one to many Division>Category
        public Guid DivisionId { get; set; }
        public Division Division { get; set; }

        [StringLength(255)]
        [Display(Name = "Requirements")]
        public string Requirements { get; set; }


    }


}
