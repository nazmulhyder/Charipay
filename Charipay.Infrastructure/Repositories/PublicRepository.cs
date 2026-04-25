using Charipay.Application.DTOs.Home;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Repositories
{
    public class PublicRepository : IPublicRepository
    {
        private readonly AppDbContext _context;
        public PublicRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<HomeStatsDto> GetHomeStatsAsync()
        {
            return new HomeStatsDto
            {
                TotalDonated = await _context.Donations
             .Where(d => d.PaymentStatus == "Succeeded")
             .SumAsync(d => d.Amount),

                TotalCampaigns = await _context.Campaigns
             .CountAsync(c => c.IsActive),

                TotalDonors = await _context.Users.Where(a=>a.UserRoles.Any(b=>b.Role.Name=="Donor"))
             .CountAsync(),

                TotalVolunteers = await _context.Users.Where(a => a.UserRoles.Any(b => b.Role.Name == "Volunteer"))
             .CountAsync(),
            };
        }
    }
}
