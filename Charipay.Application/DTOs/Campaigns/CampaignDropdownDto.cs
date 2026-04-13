using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Campaigns
{
    public class CampaignDropdownDto
    {
        public Guid CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
    }
}
