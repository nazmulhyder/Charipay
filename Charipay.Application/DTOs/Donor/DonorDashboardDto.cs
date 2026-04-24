using Charipay.Application.DTOs.Donor.Donation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Donor
{
    public class DonorDashboardDto
    {
        public double TotalDonated { get; set; }
        public int TotalDonations { get; set; }
        public int CampaignsSupported { get; set; }
        public List<RecentDonationDto> RecentDonations { get; set; } = new();
    }
}
