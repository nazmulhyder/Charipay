using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class VolunteerProgressUpdate
    {
        [Key]
        public Guid VolunteerProgressUpdateId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string UpdateText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid VolunteerUserId { get; set; }

        [ForeignKey(nameof(VolunteerUserId))]
        public VolunteerUser VolunteerUser { get; set; }
    }
}
