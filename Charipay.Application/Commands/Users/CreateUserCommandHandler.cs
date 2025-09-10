using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Users;
using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Users
{

    public class RegisterUserCommandHandler(IUnitOfWork _unitOfWork, IPasswordHasher _passwordHasher, ILogger<RegisterUserCommandHandler> logger, IMapper mapper) : IRequestHandler<CreateUserCommand, ApiResponse<UserDto>>
    {
        public async Task<ApiResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateUserCommand received for email: {Email}", request.Email);
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
           
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


            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();


            // Assign default role (User = 1)
            var userRole = new UserRole
            {
                UserID = user.UserID,
                RoleID = request.RoleID  // default "User"
            };

            await _unitOfWork.UserRoles.AddAsync(userRole);
            await _unitOfWork.SaveChangesAsync();


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
