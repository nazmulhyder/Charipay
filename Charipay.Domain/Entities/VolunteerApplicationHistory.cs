using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class VolunteerApplicationHistory
    {
        public Guid VolunteerUserId { get; set; }
        public string ActionType { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public Guid ActionByUserId { get; set; }
        public DateTime ActionAt { get; set; }
        public string Note { get; set; }
    }
}
