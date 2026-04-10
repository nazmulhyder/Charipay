using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class VolunteerTask
    {
        [Key]
        public Guid VolunteerTaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxVolunteer {  get; set; }
        

        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public bool IsActive { get; set; }
        public string? SupportType { get; set; } // Field Support, Promotion, Coordination
        public string? Requirements { get; set; }
        public string? Instructions { get; set; }

        public ICollection<VolunteerUser> VolunteerUsers { get; set; }
    }
}
