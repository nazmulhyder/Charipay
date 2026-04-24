using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using Charipay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Repositories
{
    public class DonationRepository : Repository<Donation>, IDonationRepository
    {
        private readonly AppDbContext _appDbContext;
        public DonationRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<Donation> Donation(Guid donationId, Guid UserId)
        {
            var query = await _appDbContext.Donations.Where(x=>x.DonationId == donationId && x.UserId == UserId).FirstOrDefaultAsync();

            return query;
        }

        public async Task<(List<Donation> Items, int totalCount)> Donations(Guid DonorUserId, int pageNumber, int pageSize, string? search = null)
        {
            var query = await _appDbContext.Donations
                        .Include(a => a.Campaign)
                        .ThenInclude(b => b.Charity)
                        .Where(d => d.UserId == DonorUserId)
                        .OrderByDescending(d => d.DonationDate).ToListAsync();


            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a=>a.Campaign.CampaignName.Contains(search) 
                || a.Campaign.Charity.Name.Contains(search)).ToList();
            }


            var donations = query.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();

            var totalCount = donations.Count();

            return (donations, totalCount);

        }

        public async Task<List<Donation>> GetDonationsByUserIdAsync(Guid userId)
        {
            var donations = await _appDbContext.Donations
                .Include(y=>y.Campaign)
                .ThenInclude(z=>z.Charity)
                .Where(d=>d.UserId == userId)              
                .ToListAsync();

            return donations;
        }
    }
}
