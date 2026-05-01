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

namespace Charipay.Application.Commands.Admin.Feedback
{
    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, ApiResponse<UserFeedbackDto>>
    {
        private readonly IUserFeedbackRepository _userFeedbackRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public CreateFeedbackCommandHandler(IUnitOfWork unitOfWork, IUserFeedbackRepository userFeedbackRepository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _userFeedbackRepository = userFeedbackRepository;
            _currentUserService = currentUserService;

        }
        public async Task<ApiResponse<UserFeedbackDto>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            try
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

                var result = await _userFeedbackRepository.AddAsync(feedback);
                await _unitOfWork.SaveChangesAsync();

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
            catch (Exception ex) {

                var message = ex.InnerException?.Message ?? ex.Message;
                return ApiResponse<UserFeedbackDto>.FailedResponse(message);

            }


        }
    }
}
