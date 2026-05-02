using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Admin.Dashboard.Users;
using Charipay.Application.Interfaces.Repositories;
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
        private readonly IUserRepository userRepository;

        public GetUserListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository _userRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            userRepository = _userRepository;
        }
        public async Task<ApiResponse<PageResult<AdminUserListDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var (users, totalCount) = await userRepository.GetPagedUserAsync(request.PageNumber, request.PageSize, request.Search);

            var mappedUserList = _mapper.Map<List<AdminUserListDto>>(users);

            var result = new PageResult<AdminUserListDto>(mappedUserList, totalCount, request.PageNumber, request.PageSize);

            return ApiResponse<PageResult<AdminUserListDto>>.SuccessResponse(result, "Fetched Successfully");

        }
    }

}
