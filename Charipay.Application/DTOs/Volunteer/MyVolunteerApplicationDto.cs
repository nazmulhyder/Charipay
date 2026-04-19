using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Volunteer
{
    public class MyVolunteerApplicationDto
    {
        public Guid VolunteerUserId { get; set; }
        public Guid VolunteerTaskId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string CampaignName { get; set; } = string.Empty;
        public Guid CampaignId { get; set; }

        public string CharityName { get; set; } = string.Empty;
        public Guid CharityId { get; set; }

        public DateTime SignupDate { get; set; }
        public bool IsActive { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? VolunteerMessage { get; set; }
        public string? AvailabilityNote { get; set; }
        public string? AdminNote { get; set; }

        public DateTime? ReviewedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
