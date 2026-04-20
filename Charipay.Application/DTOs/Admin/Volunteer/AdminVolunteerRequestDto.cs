using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Admin.Volunteer
{
    public class AdminVolunteerRequestDto
    {
        public Guid VolunteerUserId { get; set; }
        public Guid UserId { get; set; }
        public string VolunteerName { get; set; } = string.Empty;
        public string VolunteerEmail { get; set; } = string.Empty;

        public Guid VolunteerTaskId { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
        public string TaskLocation { get; set; } = string.Empty;
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskEndDate { get; set; }

        public Guid CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;

        public Guid CharityId { get; set; }
        public string CharityName { get; set; } = string.Empty;

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
