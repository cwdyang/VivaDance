using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    public class Participant
    {
        public Guid Id { get; set; }

        [StringLength(255)]
        [Display(Name = "Entity Name")]
        public string EntityName { get; set; }

        
        [Display(Name = "Entity Number")]
        public int EntityNumber { get; set; }

        [StringLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(255)]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [StringLength(255)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //one to many Organisation>Partipant
        public Guid OrganisationId { get; set; }
        public Organisation Organisation { get; set; }

        

    }



}
