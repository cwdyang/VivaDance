using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viva.CorporateSys.Dance.Domain.Models
{
    public class Organisation:BaseObject
    {
        public Organisation()
        {
            this.Participants = new HashSet<Participant>();
        }

        //one to many Organisation>Partipant
        public virtual ICollection<Participant> Participants { get; set; }

    }
}
