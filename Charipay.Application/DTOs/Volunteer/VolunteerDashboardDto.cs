using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Volunteer
{
    public class VolunteerDashboardDto
    {
        public string VolunteerName { get; set; } = string.Empty;
        public int TotalPendingTasks { get; set; }
        public int TotalAssignedTasks { get; set; }
        public int TotalActiveTasks { get; set; }
        public int TotalCompletedTasks { get; set; }

        public List<VolunteerTaskSummaryDto> AssignedTasks { get; set; } = new();
        public List<VolunteerTaskSummaryDto> PendingTasks { get; set; } = new();
        public List<VolunteerRecentUpdateDto> RecentUpdates { get; set; } = new();
    }
}
