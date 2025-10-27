using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Charities
{
    public class CharityDto
    {
        public Guid CharityId { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string ContactEmail { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Guid CreatedByUserId { get; set; }
    }
}
