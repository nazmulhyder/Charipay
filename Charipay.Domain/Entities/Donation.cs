using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Donation
    {
        [Key]
        public Guid DonationId { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

       
        //payment info
        public double Amount {  get; set; }
        public DateTime DonationDate { get; set; }
        public bool IsAnonymous { get; set; }

        //payment Gateway
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public string PaymentStatus { get; set; }
        public string ReceiptUrl { get; set; }

        // navigation property
        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; }

    }
}
