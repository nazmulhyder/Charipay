using AutoMapper;
using Charipay.Application.DTOs.Users;
using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Users
{
    public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await unitOfWork.Users.GetByIdAsync(request.UserId);
            if (existingUser == null) {

                throw new Exception("User not found!");
            }


            var user = mapper.Map<User>(request);

            unitOfWork.Users.Update(user);
            await unitOfWork.SaveChangesAsync();

            var updateUser = await unitOfWork.Users.GetByIdAsync(user.UserID);

            return mapper.Map<UserDto>(updateUser);

        }
    }
}
