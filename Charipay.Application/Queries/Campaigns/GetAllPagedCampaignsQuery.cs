using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Campaigns;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Campaigns
{
    public class GetAllPagedCampaignsQuery : PagedRequest, IRequest<ApiResponse<PageResult<CampaignDto>>>
    {
        public string? Search { get; set; }
    }
}
