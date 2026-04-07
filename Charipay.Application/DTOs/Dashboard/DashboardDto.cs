using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalUsers { get; set; }
        public int TotalCharities { get; set; }
        public int TotalCampaigns { get; set; }
        public int TotalDonations { get; set; }

        public List<RecentDonationDto> RecentDonations { get; set; } = new();
    }
}
