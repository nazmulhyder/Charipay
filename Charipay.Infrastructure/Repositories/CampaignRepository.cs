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

        public async Task<(IEnumerable<Campaign>, int totalCount)> GetPublicPagedCampaigns(int PageNumber, int PageSize, bool? IsFeatured,CancellationToken token, string? Search = null)
        {

            var query = _appDbContext.Campaigns.AsQueryable();

            // IsFeatured = true means all featured campaigns
            if(IsFeatured == true)
            {
                query =  query
                    .Include(x=>x.Charity)
                    .Where(
                    c => c.CampaignStartDate <= DateTime.UtcNow &&
                    c.CampaignEndDate >= DateTime.UtcNow &&  c.IsFeatured == true
                    ).OrderByDescending(x=>x.CreatedAt);
            }

            // IsFeatured = null // means all active (featured + non featured) campaigns
            var  campaigns = await query
                .Include(x => x.Charity)
                    .Where(
                    c => c.CampaignStartDate <= DateTime.UtcNow &&
                    c.CampaignEndDate >= DateTime.UtcNow && c.IsActive == true)
                .Skip((PageNumber -1) * PageSize)
                .Take(PageSize)
                .AsNoTracking()
                .ToListAsync(token);

            if (!string.IsNullOrEmpty(Search))
                campaigns = campaigns.Where(c => c.CampaignName.Contains(Search)).ToList();

            var totalCount = campaigns.Count();

            return (campaigns, totalCount);


        }

        public async Task<(IEnumerable<Campaign>, int totalCount)> GetAdminPagedCampaigns(int PageNumber, int PageSize, bool? IsFeatured, bool? IsActive, CancellationToken token, string? Search = null)
        {
            var query = _appDbContext.Campaigns.AsQueryable();

            // IsFeatured = true means all featured campaigns
            if (IsActive == true)
            {
                query = query
                    .Include(x => x.Charity)
                    .Where(
                    c => c.CampaignStartDate <= DateTime.UtcNow &&
                    c.CampaignEndDate >= DateTime.UtcNow && c.IsActive == true
                    ).OrderByDescending(x => x.CreatedAt);
            }

            // IsFeatured = null // means all active (featured + non featured) campaigns
            var campaigns = await query
                .Include(x => x.Charity)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .AsNoTracking()
                .OrderByDescending(x => x.IsActive)
                .ToListAsync(token);

            if (!string.IsNullOrEmpty(Search))
                campaigns = campaigns.Where(c => c.CampaignName.Contains(Search)).ToList();

            var totalCount = campaigns.Count();

            return (campaigns, totalCount);
        }

        public async Task<List<Campaign>> GetCampaignsByCharityAsync(Guid CharityId)
        {
            var data = await _appDbContext.Campaigns.Where(c=>c.CharityId == CharityId && c.IsActive && c.CampaignStartDate <= DateTime.UtcNow && c.CampaignEndDate >= DateTime.UtcNow)
                .OrderBy(c=>c.CampaignName).ToListAsync();

            return data;
        }


        //public async Task<(IEnumerable<Campaign>, int totalCount)> GetAllPagedCampaings(int PageNumber, int PageSize, bool IsPublic, string? Search = null)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
