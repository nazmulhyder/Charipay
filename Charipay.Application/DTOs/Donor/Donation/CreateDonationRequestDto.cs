using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Donor.Donation
{
    public class CreateDonationRequestDto
    {
        public int CampaignId { get; set; }
        public decimal Amount { get; set; }
        public string? DonorName { get; set; }
        public string? DonorEmail { get; set; }
        public string? Message { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
