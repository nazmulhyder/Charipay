using AutoMapper;
using Charipay.Application.Common.Models;
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
    public class LoginUserCommandHandler(IUnitOfWork _unitOfWork, IPasswordHasher _passwordHasher, ILogger<LoginUserCommandHandler> logger, IJwtTokenService jwtTokenService) : IRequestHandler<LoginUserCommand, ApiResponse<LoginResponseDto>>
    {
        public async Task<ApiResponse<LoginResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);

            if (user == null)
            {
                logger.LogWarning("User not found for this email: {Email} ", request.Email);

                return new ApiResponse<LoginResponseDto>()
                {
                    Success = false,
                    Message = "Invalid User Email!",
                    Data = null,
                    Errors = new List<string>() { "User not found!" }

                };
                
            }

            if(!_passwordHasher.Verify(user.PasswordHash, request.Password))

                return new ApiResponse<LoginResponseDto>()
                {
                    Success = false,
                    Message = "Invalid User Password!",
                    Data = null,
                    Errors = new List<string>() { "User not found!" }

                };

            var roles = user.UserRoles.Select(x => x.Role.Name).ToList();

            var token = jwtTokenService.GenerateToken(user, roles);

            var dto = new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                ImageUrl = user.ProfileImageUrl,
                Roles = roles

            };

            return new ApiResponse<LoginResponseDto>()
            {
                Success = true,
                Message = "Login successful",
                Data = dto
            };
        }
    }
}
