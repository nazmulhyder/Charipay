using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Donation
{
    public class DonationResponseDto
    {
        public Guid DonationId { get; set; }
        public Guid CampaignId { get; set; }
        public double Amount { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime DonationDate { get; set; }

        public string PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
        public string PaymentStatus { get; set; }
        public string? ReceiptUrl { get; set; }
    }
}
