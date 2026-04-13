using Charipay.Application.DTOs.Campaigns;
using Charipay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Interfaces.Repositories
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        public Task<bool> GetCampaignByCharityId(Guid CharityId,string CampaignName, CancellationToken token);
        public Task<(IEnumerable<Campaign>, int totalCount)> GetPublicPagedCampaigns(int PageNumber, int PageSize, bool? IsFeatured, CancellationToken token, string? Search = null);
        public Task<(IEnumerable<Campaign>, int totalCount)> GetAdminPagedCampaigns(int PageNumber, int PageSize, bool? IsFeatured, bool? IsActive, CancellationToken token, string? Search = null);
        public Task<List<Campaign>> GetCampaignsByCharityAsync(Guid CharityId);

    }
}
