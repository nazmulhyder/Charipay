using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class Charity
    {
        [Key]
        public Guid CharityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public Guid? CreatedByUserId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }

        //navigationKey
        public User CreatedByUser { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }
    }
}
