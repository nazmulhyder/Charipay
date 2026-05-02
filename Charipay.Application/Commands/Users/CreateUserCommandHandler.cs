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

    public class CreateUserCommandHandler(IUnitOfWork _unitOfWork, IPasswordHasher _passwordHasher, ILogger<CreateUserCommandHandler> logger, IMapper mapper, IUserRepository userRepository
        , IUserRoleRepository userRoleRepository) 
        : IRequestHandler<CreateUserCommand, ApiResponse<UserDto>>
    {
        public async Task<ApiResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateUserCommand received for email: {Email}", request.Email);
            var existingUser = await userRepository.GetByEmailAsync(request.Email);
           
            if (existingUser != null)
            {
                logger.LogWarning("User already exists with this email: {Email}", request.Email);
               
                return new ApiResponse<UserDto>()
                {
                    Success = false,
                    Message = "User already exists with this email!",
                    Data = null,
                    Errors = new List<string>() { "User already exists!" }

                };
            }

            var user = mapper.Map<User>(request);
            user.PasswordHash = _passwordHasher.Hash(request.Password);

            await userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();


            // Assign default role (User = 1)
            var userRole = new UserRole
            {
                UserID = user.UserID,
                RoleID = request.RoleID  // default "User"
            };

            await userRoleRepository.AddAsync(userRole);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            logger.LogInformation("User created successfully with ID: {Id}", user.UserID);
            
            var resultDto =  mapper.Map<UserDto>(user);

            return new ApiResponse<UserDto>()
            {
                Success = true,
                Message = "User created successfully!",
                Data = resultDto
            };
        }
    }
}
