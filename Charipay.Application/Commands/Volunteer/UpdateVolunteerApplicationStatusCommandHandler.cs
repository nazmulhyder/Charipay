using Charipay.Application.Common.Models;
using Charipay.Application.InterfaceImpl;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Volunteer
{
    public class UpdateVolunteerApplicationStatusCommandHandler 
        (IUnitOfWork _unitOfWork, ICurrentUserService _currentUserService, IVolunteerUserRepository volunteerUser, ILogger<UpdateVolunteerApplicationStatusCommandHandler> logger)
        : IRequestHandler<UpdateVolunteerApplicationStatusCommand, ApiResponse<string>>
    {
       

        public async Task<ApiResponse<string>> Handle(UpdateVolunteerApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId.Value; var currentStatus = string.Empty;

            if (userId == null)
            {
                logger.LogWarning("Volunteer application status update failed. Because, user is not authenticated. volunteer id: {userid}", request.VolunteerUserId);
                return ApiResponse<string>.FailedResponse("User is not authenticated");
            }

            var application = await volunteerUser.GetByIdAndUserIdAsync(request.VolunteerUserId, userId); currentStatus = application?.Status.ToString();

            if (application == null)
            {
                logger.LogWarning("Volunteer application status update failed. Because, application is not found. Volunteer ID: {volunteerid}, Task title :{tasktitle}", request.VolunteerUserId, application.VolunteerTask.Title);

                return ApiResponse<string>.FailedResponse("Application not found!");
            }

            switch(request.Action)
            {
                case VolunteerApplicationAction.Started:
                    if (application.Status.ToLower() != VolunteerApplicationAction.Approved.ToString().ToLower())
                    {
                        logger.LogWarning("Volunteer application status update failed. Because, Only approved applications can be eligible for start. Volunteer ID: {volunteerid}, Task title :{tasktitle} , from status: {fromStatus}, to status {tostatus}"
                            , request.VolunteerUserId, application.VolunteerTask.Title, application.Status, "Start");
                        return ApiResponse<string>.FailedResponse("Only approved applications can be start");
                    }

                    logger.LogInformation("Volunteer application status change request started. Volunteer ID: {volunteerid}, Task title :{tasktitle}", request.VolunteerUserId, application.VolunteerTask.Title);

                    application.Status = VolunteerApplicationAction.Started.ToString();
                        application.StartedAt = DateTime.UtcNow;
                        break;

                case VolunteerApplicationAction.Cancelled:
                    if (application.Status.ToLower() is "completed" or "rejected" or "cancelled")
                    {
                        logger.LogWarning("Volunteer application status update failed. Because, only completed/rejected application can not be cancelled. Volunteer ID: {volunteerid}, Task title :{tasktitle} , from status: {fromStatus}, to status {tostatus}"
                            , request.VolunteerUserId, application.VolunteerTask.Title, application.Status, "Cancelled");
                        return ApiResponse<string>.FailedResponse("Completed/rejected application can not be cancelled");
                    }

                    if (application.Status.ToLower() is "pending" or "approved")
                    {
                        logger.LogInformation("Volunteer application status change request started. Volunteer ID: {volunteerid}, Task title :{tasktitle}, from status: {fromStatus}, to status {tostatus}"
                            , request.VolunteerUserId, application.VolunteerTask.Title, application.Status, "Cancelled");

                        application.Status = VolunteerApplicationAction.Cancelled.ToString();
                        application.StartedAt = DateTime.UtcNow;
                    }

                    else if (application.Status.ToLower() is "started")
                    {
                        logger.LogInformation("Volunteer application status change request started. Volunteer ID: {volunteerid}, Task title :{tasktitle} , from status: {fromStatus}, to status {tostatus}"
                            , request.VolunteerUserId, application.VolunteerTask.Title, application.Status, "WithdrawalRequested");

                        application.Status = VolunteerApplicationAction.WithdrawalRequested.ToString();
                        application.StartedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        logger.LogWarning("Can not withdraw this application. Unknown error");
                        return ApiResponse<string>.FailedResponse("Can not withdraw this application.");
                    }
                   
                    break;

                  case  VolunteerApplicationAction.CompletionRequested:

                    if (application.Status.ToLower() is "started")
                    {
                        logger.LogInformation("Volunteer application status change request started. Volunteer ID: {volunteerid}, Task title :{tasktitle} , from status: {fromStatus}, to status {tostatus}"
                            , request.VolunteerUserId, application.VolunteerTask.Title, application.Status, "started");
                        application.Status = VolunteerApplicationAction.CompletionRequested.ToString();
                        application.StartedAt = DateTime.UtcNow;
                    }
                        
                    else
                    {
                        logger.LogWarning("Application cannot be completed. Unknown error");
                        return ApiResponse<string>.FailedResponse("Application cannot be completed");
                    }

                   break;

                  default:
                    {
                        logger.LogWarning("Invalid volunteer action.");
                        return ApiResponse<string>.FailedResponse("Invalid volunteer action.");
                    }

                 }

            await _unitOfWork.SaveChangesAsync();
            logger.LogInformation("Volunteer application status updated successfully. Volunteer ID: {volunteerid}, Task title :{tasktitle} , from status: {fromStatus}, to status {tostatus}"
                            , request.VolunteerUserId, application.VolunteerTask.Title, currentStatus, application.Status);


            return ApiResponse<string>.SuccessResponse(null, "Volunteer application status updated successfully.");


        }
    }
}
