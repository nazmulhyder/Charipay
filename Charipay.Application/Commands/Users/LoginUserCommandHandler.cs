using AutoMapper;
using Charipay.Application.DTOs.Users;
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
    public class LoginUserCommandHandler(IUnitOfWork _unitOfWork, IPasswordHasher _passwordHasher, ILogger<RegisterUserCommandHandler> logger, IJwtTokenService jwtTokenService) : IRequestHandler<LoginUserCommand, LoginResultDto>
    {
        public async Task<LoginResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);

            if (user == null)
            {
                logger.LogWarning("User not found for this email: {Email} ", request.Email);
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            if(!_passwordHasher.Verify(user.PasswordHash, request.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            var roles = user.UserRoles.Select(x => x.Role.Name).ToList();

            var token = jwtTokenService.GenerateToken(user, roles);

            return new LoginResultDto
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                ImageUrl = user.ProfileImageUrl,
                Roles = roles
                
            };
        }
    }
}
