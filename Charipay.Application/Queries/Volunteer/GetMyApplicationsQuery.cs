using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Volunteer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Volunteer
{
    public class GetMyApplicationsQuery  : PagedRequest, IRequest<ApiResponse<PageResult<MyVolunteerApplicationDto>>>
    {
        public string? Search {  get; set; }
        public string? status { get; set; }
    }
}
