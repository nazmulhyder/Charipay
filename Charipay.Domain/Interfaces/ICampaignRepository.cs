using Charipay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Interfaces
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        public Task<bool> GetCampaignByCharityId(Guid CharityId,string CampaignName, CancellationToken token);
        public Task<(IEnumerable<Campaign>, int totalCount)> GetAllPagedCampaings(int PageNumber, int PageSize, string? Search = null);
    }
}
