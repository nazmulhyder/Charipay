using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.DTOs.Volunteer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Volunteer
{
    public class GetVolunteerOpportunitiesQuery : PagedRequest, IRequest<ApiResponse<PageResult<VolunteerOpportunityDto>>>
    {
        public string? Search { get; set; }
    }
}
