using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Dashboard
{
    public class RecentDonationDto
    {
        public string DonorName { get; set; }
        public string CampaignName { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
