using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Donor.Donation
{
    public class RecentDonationDto
    {
        public Guid DonationId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public string CharityName { get; set; } = string.Empty;
        public double Amount { get; set; }
        public DateTime DonationDate { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
    }
}
