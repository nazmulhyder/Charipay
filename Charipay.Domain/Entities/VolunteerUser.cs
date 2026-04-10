using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class VolunteerUser
    {
        [Key]
        public Guid VolunteerUserId { get; set; }
        public DateTime SignupDate { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected, Active, Completed, Withdrawn
        public DateTime? ReviewedAt { get; set; }
        public Guid? ReviewedByAdminId { get; set; }
        public string? AdminNote { get; set; }
        public string? VolunteerMessage { get; set; }
        public string? AvailabilityNote { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public Guid VolunteerTaskId { get; set; }

        [ForeignKey(nameof(VolunteerTaskId))]
        public VolunteerTask VolunteerTask { get; set; }

        public ICollection<VolunteerProgressUpdate> volunteerProgressUpdates { get; set; } = new List<VolunteerProgressUpdate>();
    }
}
