using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class VolunteerUser
    {
        [Key]
        public Guid VolunteerUserId { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid VolunteerTaskId { get; set; }
        public VolunteerTask VolunteerTask { get; set; }

        public DateTime SignupDate { get; set; }
        public bool IsActive { get; set; }
    }
}
