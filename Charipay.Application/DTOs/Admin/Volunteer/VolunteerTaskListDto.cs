using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Admin.Volunteer
{
    public class VolunteerTaskDto : CreateVolunteerTaskDto
    {
        public Guid VolunteerTaskId { get; set; }
        public Guid CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public Guid CharityId { get; set; }
        public string CharityName { get; set; } = string.Empty;
    }
}
