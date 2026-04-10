using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Volunteer
{
    public class VolunteerRecentUpdateDto
    {
        public Guid VolunteerProgressUpdateId { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
        public string UpdateText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
