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
    }
}
