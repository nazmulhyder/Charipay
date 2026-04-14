using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Volunteer
{
    public class VolunteerOpportunityDto
    {
        public Guid VolunteerTaskId { get; set; }
        public Guid CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public Guid CharityId { get; set; }
        public string CharityName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxVolunteer { get; set; }
        public int JoinedVolunteerCount { get; set; }
        public bool IsApplied { get; set; }
      
        public int? AppliedCount { get; set; }
        public int? RemainingSlots { get; set; }
        public bool? IsFull { get; set; }
        public bool? AlreadyApplied { get; set; }
    }
}
