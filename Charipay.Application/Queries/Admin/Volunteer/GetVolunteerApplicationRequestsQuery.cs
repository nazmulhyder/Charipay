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
    public class GetVolunteerApplicationRequestsQuery :PagedRequest, IRequest<ApiResponse<PageResult<AdminVolunteerRequestDto>>>
    {
        public string? search {  get; set; }
        public string? status { get; set; }
    }
}
