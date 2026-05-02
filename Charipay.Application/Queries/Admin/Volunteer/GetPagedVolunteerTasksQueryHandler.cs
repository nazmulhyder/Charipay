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
    public class GetPagedVolunteerTasksQueryHandler : IRequestHandler<GetPagedVolunteerTasksQuery, ApiResponse<PageResult<VolunteerTaskDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVolunteerTaskRepository _volunteerTaskRepository;

        public GetPagedVolunteerTasksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IVolunteerTaskRepository volunteerTaskRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           _volunteerTaskRepository = volunteerTaskRepository;
        }

        public async Task<ApiResponse<PageResult<VolunteerTaskDto>>> Handle(GetPagedVolunteerTasksQuery request, CancellationToken cancellationToken)
        {
            var (volunteerTasks, totalCount) = await _volunteerTaskRepository.GetPagedVolunteerTaskAsync(request.PageNumber, request.PageSize, request.Search);

            var items = _mapper.Map<List<VolunteerTaskDto>>(volunteerTasks);


            var result = new PageResult<VolunteerTaskDto>(
                 items,
                 totalCount,
                 request.PageNumber,
                 request.PageSize
             );


            return ApiResponse<PageResult<VolunteerTaskDto>>.SuccessResponse( result, "Success");

        }
    }
}
