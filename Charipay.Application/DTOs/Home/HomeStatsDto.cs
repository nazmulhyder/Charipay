using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Home
{
    public class HomeStatsDto
    {
        public double TotalDonated { get; set; }
        public int TotalCampaigns { get; set; }
        public int TotalDonors { get; set; }
        public int TotalVolunteers { get; set; }
    }
}
