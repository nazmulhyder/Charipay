using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Volunteer
{
    public class VolunteerUserDto
    {
        public Guid VolunteerUserId { get; set; }
        public Guid VolunteerTaskId { get; set; }
        public Guid UserId { get; set; }

        public DateTime SignupDate { get; set; }
        public bool IsActive { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? VolunteerMessage { get; set; }
        public string? AvailabilityNote { get; set; }


    }
}
