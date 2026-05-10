using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Users
{
    public class DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, ILogger<DeleteUserCommandHandler> _logger) 
        : IRequestHandler<DeleteUserCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user == null) {

                _logger.LogWarning("User not found. UserID: {UserId}", request.UserId);
                return ApiResponse<string>.FailedResponse("User not found!");
            }


            _logger.LogInformation("User delete operation started. UserID: {UserId}, Email: {Email}", user?.UserID, user?.Email);

            userRepository.Remove(user, cancellationToken);
            await  unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User deleted successfully. UserID: {UserId}, Email: {Email}", user?.UserID, user?.Email);

            return ApiResponse<string>.SuccessResponse("User deleted successfully.");

        }
    }
}
