using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Admin.Volunteer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Admin.Volunteer
{
    public class GetPagedVolunteerTasksQuery :  PagedRequest, IRequest<ApiResponse<PageResult<VolunteerTaskListDto>>>
    {
        public string? Search { get; set; }
    }
}
