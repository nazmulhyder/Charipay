using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Admin.Dashboard.Users;
using Charipay.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Admin.Dashboard.Users
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ApiResponse<PageResult<AdminUserListDto>>>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
    

        public GetUserListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<PageResult<AdminUserListDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var (users, totalCount) = await _unitOfWork.Users.GetPagedUserAsync(request.PageNumber, request.PageSize, request.Search);

            var mappedUserList = _mapper.Map<List<AdminUserListDto>>(users);

            var result = new PageResult<AdminUserListDto>(mappedUserList, totalCount, request.PageNumber, request.PageSize);

            return ApiResponse<PageResult<AdminUserListDto>>.SuccessResponse(result, "Fetched Successfully");

        }
    }

}
