using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Volunteer
{
    public class VolunteerTaskSummaryDto
    {
        public Guid VolunteerUserId { get; set; }
        public Guid VolunteerTaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CampaignName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
