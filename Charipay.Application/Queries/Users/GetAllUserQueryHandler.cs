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
    public class GetAllUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllUserQuery, ApiResponse<List<UserDto>>>
    {
        public async Task<ApiResponse<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.Users.GetAllAsync();

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
