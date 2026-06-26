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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Charipay.Application.Commands.Users
{

    public class CreateUserCommandHandler(
        IUnitOfWork _unitOfWork,
        IPasswordHasher _passwordHasher, 
        ILogger<CreateUserCommandHandler> logger, 
        IMapper mapper, 
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository) 
        : IRequestHandler<CreateUserCommand, ApiResponse<UserDto>>
    {
        public async Task<ApiResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var existingUser = await userRepository.GetByEmailAsync(request.Email);
           
            if (existingUser != null)
            {
                logger.LogWarning("User registration failed because email already exists. Email: {Email}", request.Email);
                return ApiResponse<UserDto>.FailedResponse("User already exists");
            }
                           

            var user = mapper.Map<User>(request);
            user.PasswordHash = _passwordHasher.Hash(request.Password);

            logger.LogInformation("User registration started for Email: {Email}", request.Email);
            await userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            
            if (request.RoleID == 1)
                return ApiResponse<UserDto>.FailedResponse("Invalid role selected.");

            // Assign default role (User = 1)
            var userRole = new UserRole
            {
                UserID = user.UserID,
                RoleID = request.RoleID  // default "User"
            };

            await userRoleRepository.AddAsync(userRole);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("User created successfully. UserId: {UserId}, Email: {Email}", user.UserID, user.Email);

           
            var resultDto =  mapper.Map<UserDto>(user);

            return ApiResponse<UserDto>.SuccessResponse(resultDto, "User created successfully");
        }
    }
}
