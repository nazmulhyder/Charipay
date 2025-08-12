using AutoMapper;
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
    public class GetAllUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.Users.GetAllAsync();

            return mapper.Map<List<UserDto>>(users);
        }

    }
}
