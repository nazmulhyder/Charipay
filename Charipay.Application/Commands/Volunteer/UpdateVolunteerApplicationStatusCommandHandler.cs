using Charipay.Application.Common.Models;
using Charipay.Application.InterfaceImpl;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Volunteer
{
    internal class UpdateVolunteerApplicationStatusCommandHandler : IRequestHandler<UpdateVolunteerApplicationStatusCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        public UpdateVolunteerApplicationStatusCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<string>> Handle(UpdateVolunteerApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId.Value;

            if (userId == null)
                return ApiResponse<string>.FailedResponse("User is not authenticated");

            var application = await _unitOfWork.VolunteerUser.GetByIdAndUserIdAsync(request.VolunteerUserId, userId);

            if (application == null)
                return ApiResponse<string>.FailedResponse("Application not found!");

            switch(request.Action)
            {
                case VolunteerApplicationAction.Started:
                    if (application.Status.ToLower() != VolunteerApplicationAction.Approved.ToString().ToLower())
                        return ApiResponse<string>.FailedResponse("Only approved applications can be started");

                        application.Status = VolunteerApplicationAction.Started.ToString();
                        application.StartedAt = DateTime.UtcNow;
                        break;

                case VolunteerApplicationAction.Cancelled:
                    if (application.Status.ToLower() is "completed" or "rejected" or "cancelled")
                        return ApiResponse<string>.FailedResponse("Completed/rejected application can be cancelled");

                    if (application.Status.ToLower() is "pending" or "approved")
                    {
                        application.Status = VolunteerApplicationAction.Cancelled.ToString();
                        application.StartedAt = DateTime.UtcNow;
                    }

                    else if (application.Status.ToLower() is "started")
                    {
                        application.Status = VolunteerApplicationAction.WithdrawalRequested.ToString();
                        application.StartedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        return ApiResponse<string>.FailedResponse("Cannot withdraw this application.");
                    }
                   
                    break;

                  case  VolunteerApplicationAction.CompletionRequested:

                    if (application.Status.ToLower() is "started")
                    {
                        application.Status = VolunteerApplicationAction.CompletionRequested.ToString();
                        application.StartedAt = DateTime.UtcNow;
                    }
                        
                    else
                    {
                         return ApiResponse<string>.FailedResponse("Application cannot be completed");
                    }

                   break;

                  default:
                    return ApiResponse<string>.FailedResponse("Invalid volunteer action.");

                 }

            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<string>.SuccessResponse(null, "Volunteer application status updated successfully.");


        }
    }
}
