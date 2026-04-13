using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Admin.Volunteer
{
    public class GetPagedVolunteerTasksQueryHandler : IRequestHandler<GetPagedVolunteerTasksQuery, ApiResponse<PageResult<VolunteerTaskListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPagedVolunteerTasksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PageResult<VolunteerTaskListDto>>> Handle(GetPagedVolunteerTasksQuery request, CancellationToken cancellationToken)
        {
            var (volunteerTasks, totalCount) = await _unitOfWork.VolunteerTask.GetPagedVolunteerTaskAsync(request.PageNumber, request.PageSize, request.Search);

            var items = _mapper.Map<List<VolunteerTaskListDto>>(volunteerTasks);


            var result = new PageResult<VolunteerTaskListDto>(
                 items,
                 totalCount,
                 request.PageNumber,
                 request.PageSize
             );


            return ApiResponse<PageResult<VolunteerTaskListDto>>.SuccessResponse( result, "Success");

        }
    }
}
