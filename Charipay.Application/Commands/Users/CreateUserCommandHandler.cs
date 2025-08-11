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

    public class RegisterUserCommandHandler(IUnitOfWork _unitOfWork, IPasswordHasher _passwordHasher, ILogger<RegisterUserCommandHandler> logger) : IRequestHandler<CreateUserCommand, UserDto>
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

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                DOB = request.DOB,
                CreatedAt = DateTime.Now,
                AddressLine1 = request.AddressLine1,
                PostCode = request.PostCode,
                ProfileImageUrl = request.ProfileImageUrl,
                PhoneNumber = request.PhoneNumber
            };


            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            logger.LogInformation("User created successfully with ID: {Id}", user.UserID);
            return new UserDto
            {
                UserId = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                AddressLine1 = user.AddressLine1,
                PostCode = user.PostCode,
                ProfileImageUrl = user.ProfileImageUrl,
                DOB = user.DOB
            };

        }
    }
}
