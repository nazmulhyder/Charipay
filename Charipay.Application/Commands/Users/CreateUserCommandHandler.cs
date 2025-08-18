using AutoMapper;
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

    public class RegisterUserCommandHandler(IUnitOfWork _unitOfWork, IPasswordHasher _passwordHasher, ILogger<RegisterUserCommandHandler> logger, IMapper mapper) : IRequestHandler<CreateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateUserCommand received for email: {Email}", request.Email);
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
           
            if (existingUser != null)
            {
                logger.LogWarning("User already exists with this email: {Email}", request.Email);
                throw new Exception("User already exists!");
            }

            var user = mapper.Map<User>(request);
            user.PasswordHash = _passwordHasher.Hash(request.Password);


            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();


            // Assign default role (User = 1)
            var userRole = new UserRole
            {
                UserID = user.UserID,
                RoleID = 1 // default "User"
            };

            await _unitOfWork.UserRoles.AddAsync(userRole);
            await _unitOfWork.SaveChangesAsync();


            logger.LogInformation("User created successfully with ID: {Id}", user.UserID);
            
            return mapper.Map<UserDto>(user);

        }
    }
}
