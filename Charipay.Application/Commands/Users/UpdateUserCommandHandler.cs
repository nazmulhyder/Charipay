using AutoMapper;
using Charipay.Application.DTOs.Users;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
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
            var existingUser = await unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (existingUser == null) {

                throw new Exception("User not found!");
            }


            var user = mapper.Map<User>(request);

            unitOfWork.Users.Update(user, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            var updateUser = await unitOfWork.Users.GetByIdAsync(user.UserID, cancellationToken);

            return mapper.Map<UserDto>(updateUser);

        }
    }
}
