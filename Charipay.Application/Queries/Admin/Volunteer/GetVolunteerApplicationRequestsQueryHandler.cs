using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.DTOs.Volunteer;
using Charipay.Application.Interfaces.QueryService;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Admin.Volunteer
{
    public class GetVolunteerApplicationRequestsQueryHandler : IRequestHandler<GetVolunteerApplicationRequestsQuery, ApiResponse<PageResult<AdminVolunteerRequestDto>>>
    {

        private readonly IAdminVolunteerRequestQueryService _adminVolunteerRequestQueryService;

        public GetVolunteerApplicationRequestsQueryHandler(IAdminVolunteerRequestQueryService adminVolunteerRequestQueryService) 
        {
            _adminVolunteerRequestQueryService = adminVolunteerRequestQueryService;
        }

        public async Task<ApiResponse<PageResult<AdminVolunteerRequestDto>>> Handle(GetVolunteerApplicationRequestsQuery request, CancellationToken cancellationToken)
        {
            var (applicationRequests, totalCount) = await _adminVolunteerRequestQueryService.GetVolunteerApplicationRequestsAsync(request.PageNumber, request.PageSize, request.search, request.status);

            var result = new PageResult<AdminVolunteerRequestDto>(
                applicationRequests,
                totalCount,
                request.PageNumber,
                request.PageSize
            );


            return ApiResponse<PageResult<AdminVolunteerRequestDto>>.SuccessResponse(result, "Success");
        }
    }
}
