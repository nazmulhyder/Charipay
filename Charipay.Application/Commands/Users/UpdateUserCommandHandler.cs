using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Users;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Users
{
    public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, ILogger<UpdateUserCommandHandler> logger) : IRequestHandler<UpdateUserCommand, ApiResponse<UserDto>>
    {
        public async Task<ApiResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (existingUser == null) {

                logger.LogWarning("User not found. UserID {UserID}", request.UserId);
                return ApiResponse<UserDto>.FailedResponse("User not found!");
            }


            var user = mapper.Map<User>(request);


            logger.LogInformation("User update started. UserId: {UserId}, Email: {Email}", existingUser.UserID, existingUser.Email);

            userRepository.Update(user, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            logger.LogInformation("User updatd successfully. UserId: {UserId}, Email: {Email}", existingUser.UserID, existingUser.Email);


            var response = mapper.Map<UserDto>(user);


            return ApiResponse<UserDto>.SuccessResponse(response, "User updatd successfully");

        }
    }
}
