using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Users;
using Charipay.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Users
{
    public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserByIdQuery,ApiResponse<UserDto>>
    {
        public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(request.id);

            if (user == null) return null;

            var resultDto= mapper.Map<UserDto>(user);
            return new ApiResponse<UserDto>()
            {
                Success = true,
                Message = "Success",
                Data = resultDto

            };

        }
    }
}
