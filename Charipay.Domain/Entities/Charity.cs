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
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string ContactEmail { get; set; }
        public Guid CreatedByUserId { get; set; }
        public bool IsApproved { get; set; }

        //navigationKey
        public User CreatedByUser { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }
    }
}
