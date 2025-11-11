using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using Charipay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Repositories
{
    public class CampaignRepository : Repository<Campaign>, ICampaignRepository
    {
        private readonly AppDbContext _appDbContext;
        public CampaignRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<bool> GetCampaignByCharityId(Guid CharityId,string CampaignName, CancellationToken token)
        {
            return await _appDbContext.Campaigns
                .AnyAsync(c => c.CharityId == CharityId && c.CampaignName.ToLower() == CampaignName.ToLower());
        }

        public async Task<(IEnumerable<Campaign>, int totalCount)> GetAllPagedCampaings(int PageNumber, int PageSize, string? Search = null)
        {
            var query = await _appDbContext.Campaigns
                .Include(x=>x.Charity)
                .OrderByDescending(c=>c.CreatedAt)
                .Skip((PageNumber -1) * PageSize)
                .Take(PageSize)
                .AsNoTracking()
                .ToListAsync();

            if (!string.IsNullOrEmpty(Search))
                query = query.Where(c => c.CampaignName.Contains(Search)).ToList();

            var totalCount = query.Count();

            return (query, totalCount);


        }
                                                                                                                                                                                                                               

    }
}
