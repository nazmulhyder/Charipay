using Charipay.Application.Common.Models;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Admin.Volunteer
{
    public class ReviewVolunteerApplicationCommandHandler : IRequestHandler<ReviewVolunteerApplicationCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly ICurrentUserService _currentUserService;
        public ReviewVolunteerApplicationCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public  async Task<ApiResponse<string>> Handle(ReviewVolunteerApplicationCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId.Value;

            if (userId == null)
                return ApiResponse<string>.FailedResponse("User is not authenticated");

            var application = await _unitOfWork.VolunteerUser.GetByIdAsync(request.VolunteerUserId, cancellationToken);

            if (application == null)
                return ApiResponse<string>.FailedResponse("Application not found!");

            switch (request.Action)
            {
                case AdminVolunteerApplicationAction.Approve:
                    if (application.Status != "Pending")
                        return ApiResponse<string>.FailedResponse("Only pending applications can be approved.");

                    application.Status = "Approved";
                    application.ReviewedAt = DateTime.UtcNow;
                    application.AdminNote = request.AdminNote;
                    break;

                case AdminVolunteerApplicationAction.Reject:
                    if (application.Status != "Pending")
                        return ApiResponse<string>.FailedResponse("Only pending applications can be rejected.");

                    application.Status = "Rejected";
                    application.IsActive = false;
                    application.AdminNote = request.AdminNote;
                    application.ReviewedAt = DateTime.UtcNow;
                    break;

                case AdminVolunteerApplicationAction.Complete:
                    if (application.Status != "CompletionRequested")
                        return ApiResponse<string>.FailedResponse("Only completion requested applications can be completed.");

                    application.Status = "Completed";
                    application.CompletedAt = DateTime.UtcNow;
                    application.AdminNote = request.AdminNote;
                    break;
            }

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse($"Application {application.Status} successfully");


        }
    }
}
