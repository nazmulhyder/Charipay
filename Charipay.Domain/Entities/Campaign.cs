using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class Campaign
    {
        [Key]
        public Guid CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string CampaignDescription { get; set; }
        public double GoalAmount { get; set; }
        public double CurrentAmount { get; set; }
        public DateTime CampaignStartDate { get; set; }
        public DateTime CampaignEndDate { get; set; }
        public string ImageUrl { get; set; }
        
        public Guid CharityId { get; set; }
        public Charity Charity { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<VolunteerTask> volunteerTasks { get; set; }

    }
}
