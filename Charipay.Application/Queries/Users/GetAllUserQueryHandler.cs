using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Users;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Users
{
    public class GetAllUserQueryHandler(IMapper mapper, IUserRepository userRepository) : IRequestHandler<GetAllUserQuery, ApiResponse<List<UserDto>>>
    {
        public async Task<ApiResponse<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAllAsync(cancellationToken);

            var resultDto = mapper.Map<List<UserDto>>(users);

            return new ApiResponse<List<UserDto>>()
            {
                Success = true,
                Message = "Success",
                Data = resultDto
               
            };
        }

    }
}
