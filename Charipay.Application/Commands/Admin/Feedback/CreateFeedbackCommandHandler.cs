using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Admin.UserFeedback;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Charipay.Domain.Entities;
using Charipay.Application.Commands.Admin.UserFeedback;
using Charipay.Application.Interfaces.Common;
using Microsoft.Extensions.Logging;

namespace Charipay.Application.Commands.Admin.Feedback
{
    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, ApiResponse<UserFeedbackDto>>
    {
        private readonly IUserFeedbackRepository _userFeedbackRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<CreateFeedbackCommandHandler> _logger;

        public CreateFeedbackCommandHandler(IUnitOfWork unitOfWork, IUserFeedbackRepository userFeedbackRepository, ICurrentUserService currentUserService , ILogger<CreateFeedbackCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userFeedbackRepository = userFeedbackRepository;
            _currentUserService = currentUserService;
            _logger = logger;

        }
        public async Task<ApiResponse<UserFeedbackDto>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        { 

                var feedback = new Charipay.Domain.Entities.UserFeedback
                {
                    UserFeedbackId = Guid.NewGuid(),
                    UserId = _currentUserService.UserId,
                    Rating = request.Rating,
                    FeedbackType = request.FeedbackType,
                    Message = request.Message,
                    PageUrl = request.PageUrl,
                    CreatedAt = DateTime.UtcNow,
                    Status = Domain.Enums.FeedbackStatus.New
                };


            _logger.LogInformation("User feedback has been requested. Feedback Type: {FeedbackType}", feedback.FeedbackType.ToString());

                var result = await _userFeedbackRepository.AddAsync(feedback);
                await _unitOfWork.SaveChangesAsync();


            _logger.LogInformation("A user feedback has been created. Feedback Type: {FeedbackType}", feedback.FeedbackType.ToString());


            var response = new UserFeedbackDto
                {
                    Id = result.UserFeedbackId,
                    Rating = result.Rating,
                    FeedbackType = result.FeedbackType.ToString(),
                    Message = result.Message,
                    CreatedAt = result.CreatedAt,
                };

                return ApiResponse<UserFeedbackDto>.SuccessResponse(response, "Submitted successfully");

        }
    }
}
