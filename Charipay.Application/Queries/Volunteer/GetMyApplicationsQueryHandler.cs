using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.DTOs.Volunteer;
using Charipay.Application.InterfaceImpl;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Volunteer
{
    public class GetMyApplicationsQueryHandler : IRequestHandler<GetMyApplicationsQuery, ApiResponse<PageResult<MyVolunteerApplicationDto>>>
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly ICurrentUserService _currentUser;

        public GetMyApplicationsQueryHandler(IUnitOfWork unitofWork, ICurrentUserService currentUser)
        {
            _unitofWork = unitofWork;
            _currentUser = currentUser;
        }

        public async Task<ApiResponse<PageResult<MyVolunteerApplicationDto>>> Handle(GetMyApplicationsQuery request, CancellationToken cancellationToken)
        {

            var (applicationRequests, totalCount) = await _unitofWork.VolunteerUser.GetMyApplicationsAsync(_currentUser.UserId.Value, request.PageNumber, request.PageSize, request.Search, request.status);


            var result = new PageResult<MyVolunteerApplicationDto>(
                 applicationRequests,
                 totalCount,
                 request.PageNumber,
                 request.PageSize
             );


            return ApiResponse<PageResult<MyVolunteerApplicationDto>>.SuccessResponse(result, "Success");
        }
    }
}
