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
    public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(request.id);

            if (user == null) return null;

            return mapper.Map<UserDto>(user);

            //return new UserDto
            //{
            //    UserId = user.UserID,
            //    FullName = user.FullName,
            //    AddressLine1 = user.AddressLine1,
            //    PostCode = user.PostCode,
            //    Phone = user.PhoneNumber,
            //    DOB = user.DOB,
            //    Email = user.Email,
            //    CreatedAt = user.CreatedAt,
            //    ProfileImageUrl = user.ProfileImageUrl
            //};
        }
    }
}
